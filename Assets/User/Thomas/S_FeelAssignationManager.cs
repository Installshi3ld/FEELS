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
    public event DelegateAssign RefreshUi, ChangeButtonStatement;

    bool _valueAssigned;

    private void Start()
    {
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
                UIPanelContainer.transform.position = clickPosition;
                UIPanelContainer.SetActive(true);

                s_FeelAssignationBuilding = BuildingClickedOn.GetComponent<S_FeelAssignationBuilding>();
                RefreshUi.Invoke();
            }
        }
    }

    void HideContainer()
    {
        UIPanelContainer.SetActive(false);
        BuildingClickedOn = null;
    }

    public void AssignFeel()
    {
        S_Building s_Building = BuildingClickedOn.GetComponent<S_Building>();

        _valueAssigned = s_FeelAssignationBuilding.AssignFeels(s_Building.FeelType);
        if (_valueAssigned)
        {
            ChangeButtonStatement.Invoke();
            RefreshUi.Invoke();
        }
        _valueAssigned = false;
    }

    public void UnassignFeel()
    {
        S_Building s_Building = BuildingClickedOn.GetComponent<S_Building>();

        _valueAssigned = s_FeelAssignationBuilding.UnassignFeels(s_Building.FeelType);
        if (_valueAssigned)
        {
            ChangeButtonStatement.Invoke();
            RefreshUi.Invoke();
        }
        _valueAssigned = false;
    }
}
