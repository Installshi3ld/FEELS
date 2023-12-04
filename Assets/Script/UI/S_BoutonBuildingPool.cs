using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_BoutonBuildingPool : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //[NonSerialized]
    public GameObject BuildingReference
    {
        get {
            return _BuildingReference;
        }
        set {
            _BuildingReference = value; 
            buildingScript = _BuildingReference.GetComponent<S_Building>();
            s_BuildingManager = _BuildingReference.GetComponent<S_BuildingManager>();
        }
    }

    private GameObject _BuildingReference;

    [NonSerialized]
    public S_BuildingPool _buildingPool;

    S_Building buildingScript;
    S_BuildingManager s_BuildingManager;

    public void OnPointerEnter(PointerEventData eventData)
    {

        int feelCost, increaseDecreseEquilibrium;

        feelCost = buildingScript ? buildingScript.price : 0;
        increaseDecreseEquilibrium = s_BuildingManager ? s_BuildingManager.increaseOrDecreaseAmount : 0;

        if (_buildingPool)
        {
            if(buildingScript)
                _buildingPool.SetInfoFeel(buildingScript.FeelCurrency,  feelCost);
            else
                _buildingPool.SetInfoFeel(null, feelCost);

            if (s_BuildingManager)
                _buildingPool.SetInfoEquilibrium(buildingScript.GetComponent<S_BuildingManager>().emotionType, increaseDecreseEquilibrium);
            else
                _buildingPool.SetInfoEquilibrium(null, increaseDecreseEquilibrium);

            _buildingPool.ShowInformation(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_buildingPool != null) 
            _buildingPool.ShowInformation(false);
    }

}