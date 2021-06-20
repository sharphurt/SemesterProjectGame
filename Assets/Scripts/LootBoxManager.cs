using System;
using System.Collections.Generic;
using System.Linq;
using CarParts;
using Controllers;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

public class LootBoxManager : MonoBehaviour
{
    private List<CarPartData> lootBoxData = new List<CarPartData>();

    public GameObject popup;
    public GameObject notEnoughMoneyPopUp;
    public GameObject lootBoxButton;
    public Text costText;
    public Text coins;
    public int cost;
    public GameObject partsBar;

    private CarPart part;

    public void Start()
    {
        lootBoxData = LootBoxDataLoader.Load();
        costText.text = cost.ToString();
        coins.text = Vars.Coins.ToString();
        RenderPartsBar();
    }

    public void Buy()
    {
        if (Vars.Coins < cost)
        {
            notEnoughMoneyPopUp.SetActive(true);
            return;
        }

        popup.SetActive(true);
        part = SelectPossiblePart(NormalizeChances(lootBoxData), Random.value);
        Vars.Coins -= cost;
        var image = Resources.Load<Image>($"Prefabs/Parts/{part.PrefabName}");
        popup.transform.Find("Image").gameObject.GetComponent<Image>().sprite = image.sprite;
        popup.transform.Find("Name").gameObject.GetComponent<Text>().text = part.PartName;
        popup.transform.Find("Description").gameObject.GetComponent<Text>().text = part.Description;
    }

    public void ClosePartInfoPopUp()
    {
        popup.SetActive(false);
        Vars.CarParts = Vars.CarParts.Append(part).ToList();
        RenderPartsBar();
    }

    public void CloseNotEnoughMoneyPopUp()
    {
        notEnoughMoneyPopUp.SetActive(false);
    }

    public void SellPart()
    {
    }

    private void RenderPartsBar()
    {
        foreach (Transform child in partsBar.transform)
            Destroy(child.gameObject);

        foreach (var prefab in Vars.CarParts.Select(part =>
            Resources.Load<GameObject>($"Prefabs/Parts/{part.PrefabName}")))
            Instantiate(prefab, partsBar.transform);
    }

    private List<CarPart> NormalizeChances(List<CarPartData> lootTable)
    {
        var parts = lootTable.SelectMany(c => c.chances,
                (part, data) => new
                {
                    Name = part.name,
                    PartName = data.partName,
                    Description = data.description,
                    Chance = data.chance,
                    PrefabName = data.prefabName,
                    ImprovementValue = data.improvementValue
                })
            .ToList();
        var normalizedChances = parts.Normalize(e => e.Chance).ToList();

        var normalized = new List<CarPart>();

        for (var i = 0; i < normalizedChances.Count; i++)
        {
            var part = parts[i];
            var chance = normalizedChances[i] / normalizedChances.Sum();
            normalized.Add(new CarPart
            {
                PartType = (PartType) Enum.Parse(typeof(PartType), part.Name),
                Chance = chance,
                ImprovementValue = part.ImprovementValue,
                Name = part.Name,
                Description = part.Description,
                PartName = part.PartName,
                PrefabName = part.PrefabName
            });
        }

        return normalized;
    }


    private CarPart SelectPossiblePart(List<CarPart> parts, float chance)
    {
        var sorted = parts.OrderBy(e => e.Chance).ToList();

        for (var i = 0; i < sorted.Count; i++)
        {
            var chanceOfPart = sorted.Take(i).Select(e => e.Chance).Sum() + sorted[i].Chance;
            if (chanceOfPart >= chance)
                return sorted[i];
        }

        return null;
    }
}