using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "S_BuildingCostManager", menuName = "Data/SO_BuildingCostManager")]
public class S_BuildingCostManager : ScriptableObject
{
    private List<ICostReduction> reductions = new List<ICostReduction>();

    public void AddReduction(ICostReduction reduction)
    {
        reductions.Add(reduction);  
    }

    public List<FeelTypeData> GetBuildingCost(S_BuildingData buildInfos, bool shouldConsumeReduction)
    {
        var result = new List<FeelTypeData>();

        foreach(var cost in buildInfos.feelTypeCostList)
        {
            var baseCost = new FeelTypeData()
            {
                feelPrice = cost.feelPrice,
                feelTypeCurrency = cost.feelTypeCurrency,
            };
            var updatedCost = new FeelTypeData()
            {
                feelPrice = cost.feelPrice,
                feelTypeCurrency = cost.feelTypeCurrency,
            };

            for (int i = reductions.Count - 1; i >= 0; --i) 
            {
                var reduction = reductions[i];

                if(updatedCost.feelPrice > 0 && reduction.IsValidFor(buildInfos, updatedCost))
                {
                    updatedCost = reduction.ExecuteReduction(baseCost, updatedCost, buildInfos);

                    if(shouldConsumeReduction)
                    {
                        reduction.ConsumeReduction(buildInfos);
                        if (reduction.IsExpired())
                        {
                            reductions.RemoveAt(i);
                        }
                    }
                }
            }

            result.Add(updatedCost);
        }

        return result;
    }
}

public interface ICostReduction
{
    bool IsValidFor(S_BuildingData buildData, FeelTypeData cost);
    bool IsExpired();

    void ConsumeReduction(S_BuildingData buildData);

    FeelTypeData ExecuteReduction(FeelTypeData baseCost, FeelTypeData currentCost, S_BuildingData buildData);
} 
