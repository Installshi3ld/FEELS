using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

public class S_Tuto : MonoBehaviour
{
    public GameObject tutoRounds,tutoCurrency, tutoEvent, TutoBonus; //Tuto Panels
    public GameObject uiFeels, uiEndTurn, uiEvent;
    public S_BuildingList buildingList;
    public bool firstTurnEnd, checkCurrencies;
    public S_MenuData isBuilding;
    public S_TutoData tutoData;
    public S_UIRequirementCheckbox refreshUI;
    public S_BoutonBuildingPool buildingPool;

    private void Start()
    {
       if (tutoData)
       {
            tutoData.dataInfo = false;
            tutoData.dataBonus = false;
            tutoData.displayPoolInfo = true; //Was False before
            tutoData.OneTime = 0;
       }

        if (buildingList)
         buildingList.BuildingAdded += FirstBuildingPlaced;
    }

    public void FirstBuildingPlaced()
    {
        if (buildingList)
        {

             if (buildingList.builidingsInfos.Count == 1)
                {
                    tutoRounds.gameObject.SetActive(true);
                    uiEndTurn.gameObject.SetActive(true);  
                    //isBuilding.value = false;
                }
             
        }
        else
        {
            Debug.LogError("BuildingInfo is not assigned!");
        }
    }
    public void OnClickEndTurn()
    {
        if(firstTurnEnd == false)
        {
            
           // tutoCurrency.gameObject.SetActive(true);

           // if (tutoData)
           // {
              //  tutoData.dataInfoAction += ShowEventTuto;
           // }

            ShowEventTuto();
            firstTurnEnd = true;
            tutoData.displayPoolInfo = true;
        } 
    }

    public void ShowEventTuto()
    {
            tutoEvent.gameObject.SetActive(true);
        
    }

    public S_Requirement _Requirement;
    public void RefreshUITuto()
    {
        refreshUI.UpdateCheckBox(_Requirement);
    }

    public void ShowBonusPlacement()
    {
        print("Jefonctionne");
        if(tutoData.OneTime == 0)
        {
            print("c'estwin");
            TutoBonus.gameObject.SetActive(true);
            tutoData.OneTime = 1;
        }
    }
}
