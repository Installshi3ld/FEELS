using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class S_FeelAssignationManager : MonoBehaviour
{
    public GameObject UIPanelContainer;
    S_PanelFeelAssignation s_panelFeelAssignation;

    GameObject BuildingClickedOn;

    [NonSerialized]
    public S_FeelAssignationBuilding s_FeelAssignationBuilding;

    public delegate void DelegateAssign();
    public event DelegateAssign RefreshUi;

    bool _valueAssigned;

    public GameObject VFXBoostVar;
    [SerializeField]
    public static GameObject VFXBoost;

    private void Start()
    {
        VFXBoost = VFXBoostVar;
        s_panelFeelAssignation = UIPanelContainer.GetComponent<S_PanelFeelAssignation>();
        if (s_panelFeelAssignation)
            s_panelFeelAssignation.mouseExitPanel += HideContainer;
    }

    public void OnLeftClick()
    {
        if (!s_panelFeelAssignation.MouseHoverPanel)
        {
            //Get Data
            Vector3 clickPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
            {
                BuildingClickedOn = hit.collider.gameObject;
            }
            //Set data
            if (BuildingClickedOn)
            {
                s_FeelAssignationBuilding = BuildingClickedOn.GetComponent<S_FeelAssignationBuilding>();
                S_Building _Building = BuildingClickedOn.GetComponent<S_Building>();

                if (_Building.isPlaced)
                {
                    RefreshUi.Invoke();

                    UIPanelContainer.transform.position = clickPosition;
                    UIPanelContainer.SetActive(true);
                }
            }
        }
    }
    public static void SpawnVFXBoost(Transform building)
    {
        if (VFXBoost)
        {
            GameObject particleSystemInstance = Instantiate(VFXBoost, building.transform.position, Quaternion.Euler(new Vector3(-90,0,0)));

            Destroy(particleSystemInstance.gameObject, 3f);
        }
    }

    void HideContainer()
    {
        UIPanelContainer.SetActive(false);
        UIPanelContainer.transform.position = new Vector3 (-100, -100, 0);
        BuildingClickedOn = null;
    }

    public void AssignFeel()
    {
        print("Je fonctionne");
        S_Building s_Building = BuildingClickedOn.GetComponent<S_Building>();
        var prices = s_Building.GetCosts();

        _valueAssigned = s_FeelAssignationBuilding.AssignFeels(prices[0].feelTypeCurrency);
        if (_valueAssigned)
        {
            RefreshUi.Invoke();
        }
        _valueAssigned = false;
    }

    public void UnassignFeel()
    {
        S_Building s_Building = BuildingClickedOn.GetComponent<S_Building>();
        var prices = s_Building.GetCosts();

        _valueAssigned = s_FeelAssignationBuilding.UnassignFeels(prices[0].feelTypeCurrency);
        if (_valueAssigned)
        {
            RefreshUi.Invoke();
        }
        _valueAssigned = false;
    }
}
