using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_FeelAssignationUI : MonoBehaviour
{
    public TextMeshProUGUI TCurrentStoredFeel;
    public TextMeshProUGUI TMaxFeel;

    public TextMeshProUGUI TAmountProduction;
    public TextMeshProUGUI TDelayBetweenProduction;

    public Button B_Assign;
    public Button B_Unassign;

    public S_FeelAssignationManager assignManager;

    private void Start()
    {
        if(assignManager) {
            assignManager.RefreshUi += RefreshUI;
            assignManager.ChangeButtonStatement += ChangeButtonStatement;
        }
        if (B_Unassign)
            B_Unassign.interactable = false;
    }
    void RefreshUI()
    {
        if (TCurrentStoredFeel)
            TCurrentStoredFeel.text = assignManager.s_FeelAssignationBuilding.CurrentStoredFeel.ToString();
        if (TMaxFeel)
            TMaxFeel.text = assignManager.s_FeelAssignationBuilding.MaxFeel.ToString();
        if(TAmountProduction)
            TAmountProduction.text = assignManager.s_FeelAssignationBuilding.productionAmountForUI.ToString();
        if(TDelayBetweenProduction)
            TDelayBetweenProduction.text = assignManager.s_FeelAssignationBuilding.delayBetweenEachProductionForUI.ToString();
    }

    void ChangeButtonStatement()
    {
        B_Assign.interactable = !B_Assign.interactable;
        B_Unassign.interactable = !B_Unassign.interactable;
    }
}
