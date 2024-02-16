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
    public S_TutoData _TutoData;
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
            _assignationBuilding = _BuildingReference.GetComponent<S_FeelAssignationBuilding>();
            RefreshUI();
        }
    }

    private GameObject _BuildingReference;

    [NonSerialized]
    public S_BuildingPoolUI _buildingPoolUI;

    S_Building buildingScript;
    S_FeelAssignationBuilding _assignationBuilding;


    //Ajouter le spawn du building sur le listener du button
    // Reaficher les infos

    public void RefreshUI()
    {
        _buildingName.text = buildingScript.BuildingData.buildingName;
        _buildingImage.sprite = buildingScript.BuildingData.BuildingImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_TutoData.displayPoolInfo == true)
        {


            if (!_BuildingReference)
                return;

            int feelCost;

            feelCost = buildingScript ? buildingScript?.GetCosts()[0].feelPrice ?? 0 : 0;

            if (_buildingPoolUI)
            {
                if (buildingScript)
                    _buildingPoolUI.SetInfoFeel(buildingScript, _assignationBuilding);
                //.GetCosts()[0].feelTypeCurrency, feelCost);
                else
                    _buildingPoolUI.SetInfoFeel(null, null, feelCost);


                _buildingPoolUI.ShowInformation(true);
            }
            else
            {
                Debug.LogWarning("No building pool UI reference for button, abort show building data");
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_buildingPoolUI != null)
            _buildingPoolUI.ShowInformation(false);

        if (!_TutoData.dataInfo && _TutoData.dataInfoAction != null)
        {
            _TutoData.dataInfo = true;
            _TutoData.dataInfoAction.Invoke();
            print("zigouigui");
        }
        else
            print(_TutoData.dataInfo.ToString() + " Blalb" + _TutoData.dataInfoAction.ToString());

    }
}