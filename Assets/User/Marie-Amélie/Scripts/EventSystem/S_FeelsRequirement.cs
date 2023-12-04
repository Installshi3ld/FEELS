using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Feel Requirement", menuName = "Requirements/Feel")]
public class S_FeelsRequirement : S_Requirement
{
    [SerializeField] private string eventDescription;

    [SerializeField] private string constraintRequirement;

    [SerializeField] private int targetedNumberOfFeels;
    [SerializeField] private S_Currencies soFeelType;

    private void OnEnable()
    {
        NarrativeDescription = eventDescription;
        ConstraintDescription = constraintRequirement;
        HasBeenFulfilled = false;
    }

    public override bool CheckIsRequirementFulfilled()
    {
        if(soFeelType.amount >= targetedNumberOfFeels)
        {
            return true;
        }
        else { return false; }
    }
}
