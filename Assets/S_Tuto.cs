using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

public class S_Tuto : MonoBehaviour, IPointerEnterHandler
{
    public GameObject tutoRounds,tutoCurrency, tutoEvent, TutoBonus; //Tuto Panels
    public GameObject uiFeels, uiEndTurn, uiEvent;
    public S_BuildingList buildingList;
    public bool firstTurnEnd, checkCurrencies;

    private void Start()
    {

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
            tutoCurrency.gameObject.SetActive(true);
        } 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        checkCurrencies = true;
    }
}
