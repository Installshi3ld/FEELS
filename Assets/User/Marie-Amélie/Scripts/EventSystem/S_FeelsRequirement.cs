using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Feel Requirement", menuName = "Event/Feel")]
public class S_FeelsRequirement : S_Requirement
{
    [SerializeField] private string eventDescription;

    [SerializeField] private string constraintRequirement;

    [SerializeField] private List<S_Currencies> soFeelType = new List<S_Currencies>();

    [SerializeField] private List<int> targetedNumberOfFeels = new List<int>();



    [SerializeField] private List<S_Disaster> disasterConsequences = new List<S_Disaster>(); //Faire une classe tampon entre les deux pour pouvoir l'instancier ???

    private void OnEnable()
    {
        NarrativeDescription = eventDescription;
        ConstraintDescription = constraintRequirement;
        HasBeenFulfilled = false;
        LinkedDisaster = disasterConsequences;
      ;
    }

    // Adrien Modification Pensement
    public string GetMyPrivateStringRequirNmb()
    {
        
        return "/"+ targetedNumberOfFeels.ToString();
    }
    // Adrien Modification Pensement

    public override bool CheckIsRequirementFulfilled()
    {
        bool hasAllFeelsBeenAcquired = false;

        if (soFeelType.Count == targetedNumberOfFeels.Count)
        {
            int index = 0;
            hasAllFeelsBeenAcquired = true;

            foreach (S_Currencies cur in soFeelType)
            {
                if (cur.amount < targetedNumberOfFeels[index])
                {
                    hasAllFeelsBeenAcquired = false;
                }
            }
        }

        if(hasAllFeelsBeenAcquired)
        {
            Debug.Log ("Oui je suis fullfilled!");
            HasBeenFulfilled = true;
            return true;
        }
        else
        {
            Debug.Log("NOOOON je suis pas fullfilled!");
            HasBeenFulfilled = false;
            return false;
        }
    }
}
