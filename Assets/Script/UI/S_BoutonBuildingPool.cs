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
        get
        {
            return _BuildingReference;
        }
        set
        {
            _BuildingReference = value;
            buildingScript = _BuildingReference.GetComponent<S_Building>();
            s_BuildingManager = _BuildingReference.GetComponent<S_BuildingManager>();
        }
    }

    private GameObject _BuildingReference;

    [NonSerialized]
    public S_BuildingPoolUI _buildingPoolUI;

    S_Building buildingScript;
    S_BuildingManager s_BuildingManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_BuildingReference)
            return;

        int feelCost, increaseDecreseEquilibrium;

        feelCost = buildingScript ? buildingScript.GetCosts()[0].feelPrice : 0;
        increaseDecreseEquilibrium = s_BuildingManager ? s_BuildingManager.increaseOrDecreaseAmount : 0;

        if (_buildingPoolUI)
        {
            if (buildingScript)
                _buildingPoolUI.SetInfoFeel(buildingScript.GetCosts()[0].feelTypeCurrency, feelCost);
            else
                _buildingPoolUI.SetInfoFeel(null, feelCost);

            if (s_BuildingManager)
                _buildingPoolUI.SetInfoEquilibrium(buildingScript.GetComponent<S_BuildingManager>().emotionType, increaseDecreseEquilibrium);
            else
                _buildingPoolUI.SetInfoEquilibrium(null, increaseDecreseEquilibrium);

            _buildingPoolUI.ShowInformation(true);
        }
        else
        {
            Debug.LogWarning("No building pool UI reference for button, abort show building data");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_buildingPoolUI != null)
            _buildingPoolUI.ShowInformation(false);
    }

}