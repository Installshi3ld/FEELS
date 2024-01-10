using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "SO_BuildingReward", menuName = "Reward/Building")]
public class S_BuildingRewards : S_Reward
{
    [SerializeField]
    private List<S_Building> targetedBuildings = new List<S_Building>();

    [SerializeField]
    private S_BuildingCostManager costManager;
    public override void GetReward()
    {
        costManager.AddReduction(new BuildingRewardCostReduction(BuildingToBuildingData().ToList()));
    }

    private IEnumerable<S_BuildingData> BuildingToBuildingData()
    {
        foreach (var building in targetedBuildings) { yield return building.BuildingData; }
    }
}

public class BuildingRewardCostReduction : ICostReduction
{
    private List<S_BuildingData> targetBuildings = new List<S_BuildingData>();
    public BuildingRewardCostReduction(List<S_BuildingData> targetBuildings)
    {
        this.targetBuildings = new List<S_BuildingData>(targetBuildings);
    }

    public void ConsumeReduction(S_BuildingData buildData)
    {
        targetBuildings.Remove(buildData);
    }

    public FeelTypeData ExecuteReduction(FeelTypeData baseCost, FeelTypeData currentCost, S_BuildingData buildData)
    {
        return new FeelTypeData()
        {
            feelPrice = 0,
            feelTypeCurrency = baseCost.feelTypeCurrency,
        };
    }

    public bool IsExpired()
    {
        return targetBuildings.Count == 0;
    }

    public bool IsValidFor(S_BuildingData buildData, FeelTypeData cost)
    {
        return targetBuildings.Contains(buildData);
    }
}