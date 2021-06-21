using System;
using System.Linq;
using CarParts;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Configurator
{
    public class ConfiguratorManager : MonoBehaviour
    {
        public Image enginePartButton;
        public Image shootingPartButton;
        public Image armorPartButton;
        public Image damagePartButton;

        public GameObject selectPartPopUp;
        public GameObject contentBox;
        public GameObject partTemplate;


        private void Start()
        {
            enginePartButton.sprite = LoadCarPartSprite(PartType.Engine);
            shootingPartButton.sprite = LoadCarPartSprite(PartType.Shooting);
            armorPartButton.sprite = LoadCarPartSprite(PartType.Armor);
            damagePartButton.sprite = LoadCarPartSprite(PartType.Damage);
        }

        public void SelectPartButtonHandler(string partType)
        {
            selectPartPopUp.SetActive(true);
            ClearContentBox();
            FillContentBox((PartType) Enum.Parse(typeof(PartType), partType));
        }

        private void ClearContentBox()
        {
            foreach (Transform child in contentBox.transform)
                Destroy(child.gameObject);
        }

        private void FillContentBox(PartType partType)
        {
            foreach (var part in Vars.CarParts.Where(p => p.PartType == partType).OrderBy(c => c.Chance))
            {
                var prefab = Resources.Load<GameObject>($"Prefabs/Parts/{part.PrefabName}").GetComponent<Image>().sprite;
                var instance = Instantiate(partTemplate, contentBox.transform);
                instance.AddComponent(typeof(Button));
                instance.GetComponent<Button>().onClick.AddListener(() =>
                    PartButtonClickHandler(part, prefab));
                instance.transform.GetComponentsInChildren<Image>().First(c => c.gameObject.name == "Image").sprite = prefab;
                instance.transform.GetComponentsInChildren<Text>().First(c => c.gameObject.name == "Name").text =
                    part.PartName;
                instance.transform.GetComponentsInChildren<Text>().First(c => c.gameObject.name == "Description").text =
                    part.Description;
            }
        }

        private void PartButtonClickHandler(CarPart part, Sprite icon)
        {
            Vars.SaveCarPart(part);
            switch (part.PartType)
            {
                case PartType.Armor:
                    armorPartButton.sprite = icon;
                    break;
                case PartType.Damage:
                    damagePartButton.sprite = icon;
                    break;
                case PartType.Engine:
                    enginePartButton.sprite = icon;
                    break;
                case PartType.Shooting:
                    shootingPartButton.sprite = icon;
                    break;
            }

            selectPartPopUp.SetActive(false);
        }

        private Sprite LoadCarPartSprite(PartType type)
        {
            var part = Vars.GetCarPart(type);
            if (part is null)
                return Resources.Load<GameObject>($"Prefabs/Parts/{type.ToString()}_disabled").GetComponent<Image>()
                    .sprite;
            var prefab = Resources.Load<GameObject>($"Prefabs/Parts/{part.PrefabName}");
            return prefab.GetComponent<Image>().sprite;
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ClosePopUp()
        {
            selectPartPopUp.SetActive(false);
        }
    }
}