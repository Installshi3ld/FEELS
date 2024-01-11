using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_FeelAssignationUI : MonoBehaviour
{
    public TextMeshProUGUI TCurrentStoredFeel, TMaxFeel;

    public TextMeshProUGUI TAmountProduction, TDelayBetweenProduction, TBoosted;

    public Button B_Assign;
    public Button B_Unassign;

    public S_FeelAssignationManager assignManager;

    private void Start()
    {
        if(assignManager) {
            assignManager.RefreshUi += RefreshUI;
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
        if (TDelayBetweenProduction)
            TDelayBetweenProduction.text = assignManager.s_FeelAssignationBuilding.delayBetweenEachProductionForUI.ToString();

        if (TBoosted)
            if(assignManager.s_FeelAssignationBuilding.isBoosted)
                TBoosted.enabled = true;
            else
                TBoosted.enabled = false;

        ChangeButtonStatement();
    }

    void ChangeButtonStatement()
    {
        if (assignManager.s_FeelAssignationBuilding.isProducing)
        {
            B_Assign.interactable = false;
            B_Unassign.interactable = true;
        }
        else
        {
            B_Assign.interactable = true;
            B_Unassign.interactable = false;
        }
    }
}
