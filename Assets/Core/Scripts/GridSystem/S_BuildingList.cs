using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_BuildingList", menuName = "Data/BuilingList")]
public class S_BuildingList : ScriptableObject
{
    public List<S_BuildingData> builidingsInfos = new List<S_BuildingData> ();
    public Action BuildingAdded;
    public delegate void RefreshFromBuilding(FeelType buildingFeelType, int toSpawn);
    public static event RefreshFromBuilding OnBuildingAdded;
    public void AppendToBuildingList(S_BuildingData buildingInfo)
    {
        builidingsInfos.Add(buildingInfo);
        BuildingAdded?.Invoke();

        int toSpawn = 1;

        switch (buildingInfo.tier)
        {
            case 1:
                toSpawn = 1; 
                break;
            case 2: toSpawn = 2;
                break;
            case 3: toSpawn = 5;
                break;
            case 4: toSpawn = 10;
                break;
        }

        OnBuildingAdded?.Invoke(buildingInfo.feelType, toSpawn);
    }

    public void ResetOnDestroy()
    {
        builidingsInfos.Clear();
    }
}
