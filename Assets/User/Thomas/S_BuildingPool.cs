using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_BuildingPool : MonoBehaviour
{
    public List<Button> button = new List<Button>();
    public ConstructionSystem constructionSystem;

    private void Start()
    {
        constructionSystem.OnRefreshBuildingPool += RefreshUI;
    }

    void RefreshUI()
    {
        for (int i = 0; i < button.Count; i++)
        {
            button[i].image.sprite = constructionSystem.BuildingInPool[i].GetComponent<S_Building>().BuildingImage;
        }
    }

    public void RefreshPoolBuilding()
    {
        constructionSystem.RefreshBuildingPool();
    }

    public void SpawnBuilding(int Index)
    {
        constructionSystem.SpawnObject(constructionSystem.BuildingInPool[Index]);
    }

}
