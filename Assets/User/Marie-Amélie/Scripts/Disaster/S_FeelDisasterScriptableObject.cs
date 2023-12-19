using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create new disaster", menuName = "Disaster/FeelDisaster")]
public class S_FeelDisasterScriptableObject : S_Disaster
{
    [SerializeField] public List<Cost> costs = new List<Cost>();

    public override void ProvoqueDisaster()
    {
        foreach(Cost cost in costs)
        {
            cost.currency.RemoveAmount(cost.amount);
        }
    }
}

[Serializable]
public struct Cost
{
    public S_Currencies currency;
    public int amount;
}