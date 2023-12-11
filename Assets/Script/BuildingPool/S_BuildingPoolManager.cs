using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BuildingPoolManager : MonoBehaviour
{
    public ConstructionSystem constructionSystem;
    public Action RefreshUI;

    #region Variable building pool random

    public List<int> AmountOfBuildingPerTier = new List<int>();
    public List<GameObject> InitialBuildingList = new List<GameObject>();

    [NonSerialized] public List<GameObject> BuildingPool = new List<GameObject>();

    private List<List<GameObject>> buildingPerTier = new List<List<GameObject>>();

    #endregion
    
    #region Variable legacy building pool



    #endregion




    private void Start()
    {
        StoreBuildingPerTier();
        RefreshBuildingPool();
        RefreshUI();
    }

    //Building pool
    void StoreBuildingPerTier()
    {
        for (int i = 0; i < AmountOfBuildingPerTier.Count; i++)
        {
            buildingPerTier.Add(new List<GameObject>());
        }
        //Sort
        foreach (var build in InitialBuildingList)
        {
            buildingPerTier[build.GetComponent<S_Building>().BuildingData.tier].Add(build);
        }
    }

    public void RefreshBuildingPool()
    {

        BuildingPool.Clear();
        List<List<GameObject>> tmpBuildingPerTier = new List<List<GameObject>>();

        //Initiate list
        for (int i = 0; i < AmountOfBuildingPerTier.Count; i++)
        {
            tmpBuildingPerTier.Add(new List<GameObject>());
        }

        //Add element base on probability
        for (int i = 1; i < buildingPerTier.Count; i++)
        {
            for (int j = 0; j < buildingPerTier[i].Count; j++)
            {
                if (UnityEngine.Random.Range(0, 101) < buildingPerTier[i][j].GetComponent<S_Building>().probabilityToSpawnInPool)
                    tmpBuildingPerTier[i].Add(buildingPerTier[i][j]);
            }
        }
        //Add element base on limitation
        for (int i = 1; i < AmountOfBuildingPerTier.Count; ++i)
        {
            S_StaticFunc.Shuffle<GameObject>(tmpBuildingPerTier[i]);

            for (int j = 0; j < AmountOfBuildingPerTier[i]; j++)
                if (tmpBuildingPerTier[i].Count > 0)
                    BuildingPool.Add(tmpBuildingPerTier[i][j]);
        }

        //Fill with tier 0
        if (BuildingPool.Count < 8)
        {
            int index = 0;
            S_StaticFunc.Shuffle<GameObject>(buildingPerTier[0]);
            for (int i = BuildingPool.Count; i < 8; i++)
            {
                BuildingPool.Add(buildingPerTier[0][index]);
                index++;
            }
        }
        S_StaticFunc.Shuffle<GameObject>(BuildingPool);

        RefreshUI();
    }
}
