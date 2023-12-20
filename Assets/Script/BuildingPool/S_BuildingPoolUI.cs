 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_BuildingPoolUI : MonoBehaviour
{
    [SerializeField] private S_BuildingPoolManager _buildingPoolManager;
    [SerializeField] private S_BuildingPoolData _buildingPoolData;

    public List<Button> button = new List<Button>();

    //Adriano ajoute des trucs pour l'interface
    public List<TextMeshProUGUI> feelsCost = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> buildingName = new List<TextMeshProUGUI>();
    public List<Image> imageFeelType = new List<Image>();
    // C'est tout pour ici

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
            button[i].GetComponentInParent<S_BoutonBuildingPool>()._buildingPoolUI = this;
        }
    }

    public void SpawnBuilding(int Index)
    {
        GameObject _currentBuildingToSpawn = _buildingPoolData.BuildingPoolData[_buildingPoolManager.currentTierSelected][Index];

        if (_currentBuildingToSpawn)
            _buildingPoolManager.constructionSystem.SpawnObject(_currentBuildingToSpawn);
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
            GameObject _currentBuilding = _buildingPoolData.BuildingPoolData[_buildingPoolManager.currentTierSelected][i];

            if (_currentBuilding)
            {
                print(i);

                button[i].image.sprite = _currentBuilding.GetComponent<S_Building>().BuildingData.BuildingImage;
                button[i].GetComponentInParent<S_BoutonBuildingPool>().BuildingReference = _currentBuilding;

                // C'est le retour du Cirque Adriano
                buildingName[i].text = _currentBuilding.GetComponent<S_Building>().BuildingData.buildingName;
                buildingName[i].GetComponentInParent<S_BoutonBuildingPool>().BuildingReference = _currentBuilding;

                feelsCost[i].text = _currentBuilding.GetComponent<S_Building>().BuildingData.feelTypeCostList[0].feelPrice.ToString();
                feelsCost[i].GetComponentInParent<S_BoutonBuildingPool>().BuildingReference = _currentBuilding;

                // La fin du cirque
            }
            else
            {
                button[i].image.sprite = null;
                // C'est le retour du Cirque Adriano
                buildingName[i].text= null;
                feelsCost[i].text = null;
                // La fin du cirque
            }
        }
    }

}
