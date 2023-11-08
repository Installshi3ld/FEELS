using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class S_BuildingPool : MonoBehaviour
{
    public List<Button> button = new List<Button>();
    public ConstructionSystem constructionSystem;

    public GameObject InfoScreen;

    public TextMeshProUGUI T_FeelCost, T_EquilibriumCost;
    public Image FeelTypeImage, EquilibriumTypeImage;



    bool showInformation = false;
    private void Update()
    {
        if(showInformation)
            InfoScreen.transform.position = Input.mousePosition + new Vector3(-0.1f, 0.1f, 0);
    }

    private void Start()
    {
        constructionSystem.OnRefreshBuildingPool += RefreshUI;

        for (int i = 0; i < button.Count; i++)
        {
            button[i].GetComponentInParent<S_BoutonBuildingPool>()._buildingPool = this;
        }
    }

    public void ShowInformation(bool statement)
    {
        showInformation = statement;
        InfoScreen.SetActive(statement);
    }

    public void SetInfoFeel(S_Currencies FeelType, int FeelCost = 0)
    {
        FeelTypeImage.enabled = true;

        T_FeelCost.text = FeelCost.ToString();

        if(FeelType)
            FeelTypeImage.sprite = FeelType.image;
        else
            FeelTypeImage.enabled = false;
    }
    public void SetInfoEquilibrium(S_EmotionScriptableObject EquilibriumType, int EquilibriumCost = 0)
    {
        EquilibriumTypeImage.enabled = true;
        T_EquilibriumCost.text = EquilibriumCost.ToString();

        if (EquilibriumType)
            EquilibriumTypeImage.sprite = EquilibriumType.image;
        else
            EquilibriumTypeImage.enabled = false;
    }

    void RefreshUI()
    {
        for (int i = 0; i < button.Count; i++)
        {
            button[i].image.sprite = constructionSystem.BuildingInPool[i].GetComponent<S_Building>().BuildingImage;

            button[i].GetComponentInParent<S_BoutonBuildingPool>().BuildingReference = constructionSystem.BuildingInPool[i];
        }

    }

    public void RefreshPoolBuilding()
    {
        constructionSystem.RefreshBuildingPool();
    }

    public void SpawnBuilding(int Index)
    {
        constructionSystem.SpawnObject(constructionSystem.BuildingInPool[Index]);
    }

}
