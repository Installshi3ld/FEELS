using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_BoutonBuildingPool : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _buildingName;
    [SerializeField] private Image _buildingImage;
    public Button button;

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
            RefreshUI();
        }
    }

    private GameObject _BuildingReference;

    [NonSerialized]
    public S_BuildingPoolUI _buildingPoolUI;

    S_Building buildingScript;


    //Ajouter le spawn du building sur le listener du button
    // Reaficher les infos

    public void RefreshUI()
    {
        _buildingName.text = buildingScript.BuildingData.buildingName;
        _buildingImage.sprite = buildingScript.BuildingData.BuildingImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_BuildingReference)
            return;

        int feelCost;

        feelCost = buildingScript ? buildingScript?.GetCosts()[0].feelPrice ?? 0 : 0;

        if (_buildingPoolUI)
        {
            if (buildingScript)
                _buildingPoolUI.SetInfoFeel(buildingScript);
            //.GetCosts()[0].feelTypeCurrency, feelCost);
            else
                _buildingPoolUI.SetInfoFeel(null, feelCost);


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