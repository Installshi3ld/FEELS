
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SO_BuildingPoolData", menuName = "Data/SO_BuildingPoolData")]
public class S_BuildingPoolData : SerializedScriptableObject
{
    [OdinSerialize, InfoBox("Each list in Building pool is consider as Tier, based on index")]
    private List<List<GameObject>> BuildingPool;

    [ReadOnly]
    public List<List<GameObject>> BuildingPoolData;

    public void InitBuildingPoolData(int buildingPoolSizePerTier)
    {
        BuildingPoolData = new List<List<GameObject>>();

        BuildingPoolData.Clear();
        StoreBuildingPoolData(buildingPoolSizePerTier);
    }

    /// <summary>
    /// Create an instance of building pool, fill it with null based on Building Pool Size, defined in S_BuildingPoolManager
    /// </summary>
    /// <param name="_buildingPoolSize"></param>
    void StoreBuildingPoolData(int _buildingPoolSize)
    {
        for (int i = 0; i < BuildingPool.Count; i++)
        {
            List<GameObject> tmpList = new List<GameObject>();

            for(int j = 0; j < _buildingPoolSize; j++)
            {
                if(j < BuildingPool[i].Count)
                {
                    tmpList.Add(BuildingPool[i][j]);
                }
                else
                {
                    tmpList.Add(null);
                }
            }
            BuildingPoolData.Add(tmpList);
        }
    }
}
