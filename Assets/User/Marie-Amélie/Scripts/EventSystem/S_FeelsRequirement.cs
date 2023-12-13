using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_FeelRequirement", menuName = "Requirements/Feel")]
public class S_FeelsRequirement : S_Requirement
{
    [SerializeField] private List<S_Currencies> soFeelType = new List<S_Currencies>();

    [SerializeField] private List<int> targetedNumberOfFeels = new List<int>();

    public override bool CheckIsRequirementFulfilled()
    {
        bool hasAllFeelsBeenAcquired = false;

        if (soFeelType.Count == targetedNumberOfFeels.Count)
        {
            int index = 0;
            hasAllFeelsBeenAcquired = true;

            foreach (S_Currencies cur in soFeelType)
            {
                if (cur.Amount < targetedNumberOfFeels[index])
                {
                    hasAllFeelsBeenAcquired = false;
                }

                index++;
            }
        }

        HasBeenFulfilled = hasAllFeelsBeenAcquired;
        return hasAllFeelsBeenAcquired;

    }
}
