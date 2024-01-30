 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class S_BuildingPoolUI : MonoBehaviour
{
    #region Variable
    [SerializeField] private S_BuildingPoolManager _buildingPoolManager;

    [Header("Data"), SerializeField] private S_BuildingPoolData _buildingPoolData;
    public float xOffsetBetweenElement = 145;

    [Header("UI Reference")]
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GameObject PannelButton;

    [Header("Pop Up info")]
    public GameObject InfoScreen;

    //Adrien Le Vaurien passe dans le coin
    public TextMeshProUGUI T_JoyFeelCost, T_AngerFeelCost, T_SadFeelCost, T_FearFeelCost;
    public Image JoyFeelTypeImage, AngerFeelTypeImage, SadFeelTypeImage, FearFeelTypeImage;
    public TextMeshProUGUI BuildingName, ScoreInformation;
    public Image BuildingType, BuildingTheme;
    //C'est immonde je sais bien mais c'est tout pour l'instant


    private List<GameObject> button = new List<GameObject>();
    bool showInformation = false;

    [NonSerialized] public int currentTierSelected = 0;
    #endregion

    private void Start()
    {
        RefreshUI();
    }
    private void Update()
    {
        if(showInformation)
            InfoScreen.transform.position = Input.mousePosition + new Vector3(-0.1f, 0.1f, 0);
    }

    //Create button & add function reference
    void SpawnButton()
    {
        RemoveButton();
        //Spawn Buttons building pool
        float tmpOffset = -xOffsetBetweenElement/2 * (_buildingPoolData.list[currentTierSelected].Count -1);
        GameObject tmpGameobject;

        for (int i = 0; i < _buildingPoolData.list[currentTierSelected].Count; i++)
        {
            //Spawn + add offset
            tmpGameobject = Instantiate(buttonTemplate, PannelButton.transform);
            button.Add(tmpGameobject);

            // lambda take reference of variable, i variable won't work 
            int currentIndex = i;
            S_BoutonBuildingPool tmpBouton = tmpGameobject.GetComponent<S_BoutonBuildingPool>();

            tmpBouton.button.onClick.AddListener(() => SpawnBuilding(currentIndex));
            tmpBouton._buildingPoolUI = this;
        }
    }

    void RemoveButton()
    {
        button.Clear();
        foreach (Transform child in PannelButton.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ChangeCurrentTier(int _newTier)
    {
        currentTierSelected = _newTier;
        RefreshUI();
    }

    //Create building instance (based on bouton clicked in pool)
    public void SpawnBuilding(int Index)
    {
        GameObject _currentBuildingToSpawn = _buildingPoolData.list[currentTierSelected][Index];
        if (_currentBuildingToSpawn)
            _buildingPoolManager.constructionSystem.SpawnObject(_currentBuildingToSpawn);
    }

    //Show pop up information
    public void ShowInformation(bool statement)
    {
        showInformation = statement;
        InfoScreen.SetActive(statement);
    }

    //Set pop up information
    public void SetInfoFeel(S_Currencies FeelType, int FeelCost = 0)
    {
        //Adrien Modification to display the price of the 4 type of Feels
        //FeelTypeImage.enabled = true;

        T_JoyFeelCost.text = FeelCost.ToString();
        T_AngerFeelCost.text = FeelCost.ToString();
        T_SadFeelCost.text = FeelCost.ToString();
        T_FearFeelCost.text = FeelCost.ToString();

       // buildingScript = _BuildingReference.GetComponent<S_Building>();

       // BuildingName.text = _buildingPoolData.buildingName.ToString();

   //
   //   if (FeelType)
   //       FeelTypeImage.sprite = FeelType.image;
   //   else
   //       FeelTypeImage.enabled = false;
   // Fin de Modification Adrien
    }

    //Refresh building pool UI
    void RefreshUI()
    {
        SpawnButton();

        for (int i = 0; i < _buildingPoolData.list[currentTierSelected].Count; i++)
        {
            GameObject _currentBuilding = _buildingPoolData.list[currentTierSelected][i];
            button[i].GetComponentInParent<S_BoutonBuildingPool>().BuildingReference = _currentBuilding; 
        }
    }
}


/* for (int i = 0; i < button.Count; i++)
        {
            GameObject _currentBuilding = _buildingPoolData.BuildingPoolData[_buildingPoolManager.currentTierSelected][i];

            if (_currentBuilding)
            {
                //print(i);

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
        }*/