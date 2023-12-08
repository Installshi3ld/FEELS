using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_BuildingPool : MonoBehaviour
{
    [SerializeField] private S_BuildingPoolManager _buildingPoolManager;

    public List<Button> button = new List<Button>();

    public GameObject InfoScreen;

    public TextMeshProUGUI T_FeelCost, T_EquilibriumCost;
    public Image FeelTypeImage, EquilibriumTypeImage;

    bool showInformation = false;

    private void Awake()
    {
        _buildingPoolManager.RefreshUI += RefreshUI;
    }
    private void Start()
    {
        for (int i = 0; i < button.Count; i++)
        {
            button[i].GetComponentInParent<S_BoutonBuildingPool>()._buildingPool = this;
        }
    }

    public void SpawnBuilding(int Index)
    {
        _buildingPoolManager.constructionSystem.SpawnObject(_buildingPoolManager.BuildingPool[Index]);
    }

    private void Update()
    {
        if(showInformation)
            InfoScreen.transform.position = Input.mousePosition + new Vector3(-0.1f, 0.1f, 0);
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
            button[i].image.sprite = _buildingPoolManager.BuildingPool[i].GetComponent<S_Building>().BuildingData.BuildingImage;

            button[i].GetComponentInParent<S_BoutonBuildingPool>().BuildingReference = _buildingPoolManager.BuildingPool[i];
        }
    }

}
