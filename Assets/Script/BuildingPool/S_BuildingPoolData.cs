using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingPoolY
{
    public List<GameObject> buildingPoolX;
}

[CreateAssetMenu(fileName = "SO_BuildingPoolData", menuName = "Data/SO_BuildingPoolData")]
public class S_BuildingPoolData : ScriptableObject
{
    public List<BuildingPoolY> buildingPool;

    public List<List<S_BuildingData>> BuildingPoolData;

    public void InitBuildingPoolData()
    {
        BuildingPoolData.Clear();
        StoreBuildingPoolData();
    }

    void StoreBuildingPoolData()
    {
        /*
        for (int i = 0; i < BuildingPool.Length; i++)
        {
            List<S_BuildingData> tmpList = new List<S_BuildingData>();

            for(int j = 0; j < BuildingPoolData[i].Count; j++)
            {
                tmpList.Add(BuildingPool[i][j].GetComponent<S_Building>().BuildingData);
            }
            BuildingPoolData.Add(tmpList);
        }
        */
    }
}
