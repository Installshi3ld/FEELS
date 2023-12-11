
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SO_BuildingPoolData", menuName = "Data/SO_BuildingPoolData")]
public class S_BuildingPoolData : SerializedScriptableObject
{
    [OdinSerialize, InfoBox("Each list in Building pool is consider as Tier, based on index")]
    public List<List<GameObject>> BuildingPool;

    [ReadOnly]
    public List<List<S_BuildingData>> BuildingPoolData;

    public void InitBuildingPoolData()
    {
        BuildingPoolData = new List<List<S_BuildingData>>();

        BuildingPoolData.Clear();
        StoreBuildingPoolData();
        Debug.Log(BuildingPoolData.Count);
    }

    void StoreBuildingPoolData()
    {
        for (int i = 0; i < BuildingPool.Count; i++)
        {
            List<S_BuildingData> tmpList = new List<S_BuildingData>();

            for(int j = 0; j < BuildingPool[i].Count; j++)
            {
                tmpList.Add(BuildingPool[i][j].GetComponent<S_Building>().BuildingData);
            }
            BuildingPoolData.Add(tmpList);
        }
    }
}
