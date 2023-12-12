using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            if (!IsRequirementBuildingOnMap(check, themeRequirements.Count))
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
public struct ThemeRequirementMatch
{
    public BuildingTheme buildingTheme;
    public int numberOfRequiredBuildings;
}