using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_BuildThemeAndTypeRequirement", menuName = "Requirements/Building Theme And Type")]
public class S_ThemeAndTypeRequirement : S_BuildingRequirement
{
    [SerializeField]
    private List<ThemeAndTypeRequirementMatch> themeAndTypeRequirements;

    public override bool CheckIsRequirementFulfilled()
    {
        foreach (var typeRequirement in themeAndTypeRequirements)
        {
            bool check(S_BuildingData buildingData)
            {
                return buildingData.feelType == typeRequirement.feelType && buildingData.BuildingTheme == typeRequirement.buildingTheme;
            }

            //if (IsRequirementBuildingOnMap(building /*param name */ => building.feelType == typeRequirement.feelType, typeRequirement.numberOfRequiredBuildings);
            if (!IsRequirementBuildingOnMap(check, typeRequirement.numberOfRequiredBuildings, typeRequirement.numberOfBuildingAlreadyOnMap))

            {
                HasBeenFulfilled = false;
                return false;
            }
        }

        HasBeenFulfilled = true;
        return HasBeenFulfilled;
    }

    public override void DoSomethingAtFirst()
    {
        foreach (var themeAndTypeRequirement in themeAndTypeRequirements)
        {
            bool check(S_BuildingData buildingData)
            {
                return buildingData.feelType == themeAndTypeRequirement.feelType && buildingData.BuildingTheme == themeAndTypeRequirement.buildingTheme;
            }

            foreach (var building in buildingsOnMap.builidingsInfos)
            {
                if (check(building))
                {
                    themeAndTypeRequirement.numberOfBuildingAlreadyOnMap++;
                    //Debug.Log("counter " +);
                }
            }
        }

    }
}

[Serializable]
public class ThemeAndTypeRequirementMatch
{
    public FeelType feelType;
    public BuildingTheme buildingTheme;
    public int numberOfRequiredBuildings;

    [NonSerialized]
    public int numberOfBuildingAlreadyOnMap;
}
