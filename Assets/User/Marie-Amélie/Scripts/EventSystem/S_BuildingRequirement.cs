using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_BuildingRequirement : S_Requirement
{
    [SerializeField]
    protected S_BuildingList buildingsOnMap;
    protected bool IsRequirementBuildingOnMap(Func<S_BuildingData, bool> filter, int required_quantity, int wasAlreadyOnMap)
    {
        int counter = 0;

        foreach(var building in buildingsOnMap.builidingsInfos)
        {
            if (filter(building))
            {
                counter++;
                Debug.Log("counter " + counter);
            }
        }

        return counter - wasAlreadyOnMap >= required_quantity;
    }
}
