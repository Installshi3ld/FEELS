using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_BuildingRequirement : S_Requirement
{
    [SerializeField] private string eventDescription;
    public string narrativeDescription => eventDescription;

    [SerializeField] private string constraintRequirement;
    public string constraintDescription => constraintRequirement;

    [SerializeField]
    private S_BuildingList buildingsOnMap;

    private bool isFulfilled = false;
    public bool hasBeenFulfilled => isFulfilled;

    protected bool IsRequirementBuildingOnMap(Func<S_BuildingData, bool> filter, int required_quantity)
    {
        int counter = 0;

        foreach(var building in buildingsOnMap.builidingsInfos)
        {
            if (filter(building))
            {
                counter++;
            }
        }

        return counter >= required_quantity;
    }
}
