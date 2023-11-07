using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_BoutonBuildingPool : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [NonSerialized]
    public GameObject BuildingReference;
    [NonSerialized]
    public S_BuildingPool _buildingPool;

    public void OnPointerEnter(PointerEventData eventData)
    {
        S_Building buildingScript = BuildingReference.GetComponent<S_Building>();
        S_BuildingManager s_BuildingManager = BuildingReference.GetComponent<S_BuildingManager>();

        int feelCost, increaseDecreseEquilibrium;

        feelCost = buildingScript ? buildingScript.price : 0;
        increaseDecreseEquilibrium = s_BuildingManager ? s_BuildingManager.increaseOrDecreaseAmount : 0;

        if (_buildingPool)
        {
            _buildingPool.SetInfo(buildingScript.FeelType, buildingScript.GetComponent<S_BuildingManager>().emotionType,  feelCost, increaseDecreseEquilibrium);
            _buildingPool.ShowInformation(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_buildingPool != null) 
            _buildingPool.ShowInformation(false);
    }

}