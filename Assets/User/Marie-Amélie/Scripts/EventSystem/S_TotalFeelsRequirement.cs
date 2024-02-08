using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TotalFeelsRequirement", menuName = "Event/TotalFeel")]
public class S_TotalFeelsRequirement : S_Requirement
{
    [SerializeField] private string eventDescription;

    [SerializeField] private string constraintRequirement;

    [SerializeField] private List<S_Currencies> allFeels = new List<S_Currencies>();

    [SerializeField] private int targetedNumberOfFeels;

    [SerializeField] private List<S_Disaster> disasterConsequences = new List<S_Disaster>(); //Faire une classe tampon entre les deux pour pouvoir l'instancier ???

    private void OnEnable()
    {
        NarrativeDescription = eventDescription;
        ConstraintDescription = constraintRequirement;
        HasBeenFulfilled = false;
        LinkedDisaster = disasterConsequences;
    }

    public override bool CheckIsRequirementFulfilled()
    {
        int totalAmount = 0;

        foreach (S_Currencies cur in allFeels)
        {
            totalAmount += cur.Amount;
        }

        if(totalAmount >= targetedNumberOfFeels)
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

    public override void DoSomethingAtFirst()
    {
        //
    }
}
