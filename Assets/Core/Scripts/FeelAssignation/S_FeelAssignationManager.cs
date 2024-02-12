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

    //Ajout Naudar
    private static MonoBehaviour coroutineContainer; 
    //Fin Naudar

    private void Start()
    {
        VFXBoost = VFXBoostVar;
        s_panelFeelAssignation = UIPanelContainer.GetComponent<S_PanelFeelAssignation>();
        if (s_panelFeelAssignation)
            s_panelFeelAssignation.mouseExitPanel += HideContainer;

    }


 public static void SpawnVFXBoost(Transform building, GameObject VFX)
    {
        if (VFX)
        {
           GameObject particleSystemInstance = Instantiate(VFX, building.transform.position, Quaternion.Euler(new Vector3(0,0,0)));
  
           Destroy(particleSystemInstance.gameObject, 10f);
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
