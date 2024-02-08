using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


[CreateAssetMenu(fileName = "SO_BuildThemeRequirement", menuName = "Requirements/Building Theme")]
public class S_ThemeRequirement : S_BuildingRequirement
{
    [SerializeField]
    private List<ThemeRequirementMatch> themeRequirements;

    public override bool CheckIsRequirementFulfilled()
    {
        foreach (var themeRequirement in themeRequirements)
        {
            bool check(S_BuildingData buildingData)
            {
                return buildingData.BuildingTheme == themeRequirement.buildingTheme;
            }

            //if (IsRequirementBuildingOnMap(building /*param name */ => building.feelType == typeRequirement.feelType, typeRequirement.numberOfRequiredBuildings);
            if (!IsRequirementBuildingOnMap(check, themeRequirement.numberOfRequiredBuildings, themeRequirement.numberOfBuildingAlreadyOnMap))
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
        foreach (var themeRequirement in themeRequirements)
        {
            bool check(S_BuildingData buildingData)
            {
                return buildingData.BuildingTheme == themeRequirement.buildingTheme;
            }

            foreach (var building in buildingsOnMap.builidingsInfos)
            {
                if (check(building))
                {
                    themeRequirement.numberOfBuildingAlreadyOnMap++;
                    //Debug.Log("counter " +);
                }
            }
        }

    }
}



[Serializable]
public class ThemeRequirementMatch
{
    public BuildingTheme buildingTheme;
    public int numberOfRequiredBuildings;

    [NonSerialized]
    public int numberOfBuildingAlreadyOnMap;
}