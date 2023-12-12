using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_BuildTypeRequirement", menuName = "Requirements/Building Type")]
public class S_TypeRequirement : S_BuildingRequirement
{
    [SerializeField]
    private List<TypeRequirementMatch> typeRequirements;

    public override bool CheckIsRequirementFulfilled()
    {
        foreach(var typeRequirement in typeRequirements)
        {
            bool check(S_BuildingData buildingData)
            {
                return buildingData.feelType == typeRequirement.feelType;
            }

            //if (IsRequirementBuildingOnMap(building /*param name */ => building.feelType == typeRequirement.feelType, typeRequirement.numberOfRequiredBuildings);
            if (!IsRequirementBuildingOnMap(check, typeRequirements.Count))
            {
                HasBeenFulfilled = false;
                return false;
            }
        }

        HasBeenFulfilled = true;
        return HasBeenFulfilled;
    }

}

[Serializable]
public struct TypeRequirementMatch
{
    public FeelType feelType;
    public int numberOfRequiredBuildings;
}