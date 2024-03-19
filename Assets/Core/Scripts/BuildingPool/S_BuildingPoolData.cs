
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SO_BuildingPoolData", menuName = "Data/SO_BuildingPoolData")]
public class S_BuildingPoolData : SerializedScriptableObject
{
    public struct BuildingPoolStruct 
    {
       public GameObject building;
       public bool showInUI;

    }

    [OdinSerialize, InfoBox("Each list in Building pool is consider as Tier, based on index")]
    public List<List<BuildingPoolStruct>> list;

    //Adrien 

    public void SetShowInUI(int tierIndex, int elementIndex, bool showInUI)
    {
        if (tierIndex >= 0 && tierIndex < list.Count && elementIndex >= 0 && elementIndex < list[tierIndex].Count)
        {
             // R�cup�rez l'�l�ment de la liste
            BuildingPoolStruct element = list[tierIndex][elementIndex];
            // Modifiez le bool�en showInUI de cet �l�ment
            element.showInUI = showInUI;
            // Remettez l'�l�ment modifi� dans la liste
            list[tierIndex][elementIndex] = element;
        }
    }


    /*
    [ReadOnly]
    //public List<List<GameObject>> BuildingPoolData;

    public void InitBuildingPoolData(int buildingPoolSizePerTier)
    {
        //BuildingPoolData = new List<List<GameObject>>();

        BuildingPoolData.Clear();
        //StoreBuildingPoolData(buildingPoolSizePerTier);
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
            }
            BuildingPoolData.Add(tmpList);
        }
    }*/
}
