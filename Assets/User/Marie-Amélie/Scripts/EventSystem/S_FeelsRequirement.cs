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

    [SerializeField] private List<S_Disaster> disasterConsequences = new List<S_Disaster>(); // Faire une classe tampon entre les deux pour pouvoir l'instancier ???

    private void OnEnable()
    {
        NarrativeDescription = eventDescription;
        ConstraintDescription = constraintRequirement;
        HasBeenFulfilled = false;
        LinkedDisaster = disasterConsequences;
    }

    public override bool CheckIsRequirementFulfilled()
    {
        if(soFeelType.amount >= targetedNumberOfFeels)
        {
            HasBeenFulfilled = true;
            return true;
        }
        else
        {
            HasBeenFulfilled = false;
            return false;
        }
    }
}
