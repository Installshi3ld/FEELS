using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_BuildingList", menuName = "Data/BuilingList")]
public class S_BuildingList : ScriptableObject
{
    public List<S_BuildingData> builidingsInfos = new List<S_BuildingData> ();
    public Action BuildingAdded;
    public void AppendToBuildingList(S_BuildingData buildingInfo)
    {
        builidingsInfos.Add(buildingInfo);
        BuildingAdded?.Invoke();
    }

    public void ResetOnDestroy()
    {
        builidingsInfos.Clear();
    }
}
