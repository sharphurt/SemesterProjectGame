using System;
using System.Collections.Generic;
using System.Linq;
using CarParts;
using Controllers;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

public class LootBoxManager : MonoBehaviour
{
    private List<CarPartData> lootBoxData = new List<CarPartData>();

    public GameObject popup;
    public Text costText;
    public int cost;
    public GameObject partsBar;

    private CarPart part;

    public void Start()
    {
        lootBoxData = LootBoxDataLoader.Load();
        costText.text = cost.ToString();
        RenderPartsBar();
    }

    public void Buy()
    {
        popup.SetActive(true);
        part = SelectPossiblePart(NormalizeChances(lootBoxData), Random.value);
        GameInformation.score -= cost;
        var image = Resources.Load<Image>($"Prefabs/Parts/{part.prefabName}");
        popup.transform.Find("Image").gameObject.GetComponent<Image>().sprite = image.sprite;
        popup.transform.Find("Name").gameObject.GetComponent<Text>().text = part.partName;
        popup.transform.Find("Description").gameObject.GetComponent<Text>().text = part.description;
    }

    public void ClosePopUp()
    {
        popup.SetActive(false);
        GameInformation.playerParts.Add(part);
        RenderPartsBar();
    }

    public void SellPart()
    {
    }

    private void RenderPartsBar()
    {
        foreach (Transform child in partsBar.transform)
            Destroy(child.gameObject);

        foreach (var prefab in GameInformation.playerParts.Select(part =>
            Resources.Load<GameObject>($"Prefabs/Parts/{part.prefabName}"))) 
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
                partType = (PartType) Enum.Parse(typeof(PartType), part.Name),
                chance = chance,
                improvementValue = part.ImprovementValue,
                name = part.Name,
                description = part.Description,
                partName = part.PartName,
                prefabName = part.PrefabName
            });
        }

        return normalized;
    }


    private CarPart SelectPossiblePart(List<CarPart> parts, float chance)
    {
        var sorted = parts.OrderBy(e => e.chance).ToList();

        for (var i = 0; i < sorted.Count; i++)
        {
            var chanceOfPart = sorted.Take(i).Select(e => e.chance).Sum() + sorted[i].chance;
            if (chanceOfPart >= chance)
                return sorted[i];
        }

        return null;
    }
}