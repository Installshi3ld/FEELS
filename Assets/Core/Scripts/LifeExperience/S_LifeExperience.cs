using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;

public class S_LifeExperience : MonoBehaviour
{
    public GameObject smallFire;
    public GameObject wonderBuilding;
    private bool isFirePropagating;

    public S_BuildingList _BuildingList;

    List<GameObject> allFire = new List<GameObject>();
    List<Vector2Int> gridUsage = new List<Vector2Int>();

    public float delayBeforeFlamePropagation = 5;

    [SerializeField] public S_GridData _gridData = default(S_GridData);
    [SerializeField] private S_ScriptableRounds _scriptableRounds;
    private void Awake()
    {
        wonderBuilding.GetComponent<S_Building>().isPlacedAnimation = true;
        _scriptableRounds.OnActionPointRemoved += FlamePropagation;
    }
    private void Start()
    {
        S_Building building = wonderBuilding.GetComponent<S_Building>();
        this.transform.position = _gridData.GetRandomTileInGrid(building.tilesCoordinate) + new Vector3(0, 50, 0);

        if (this.transform.position.y <= -500)
            Destroy(this);

        
        Vector2Int tmpIndex = _gridData.GetIndexbasedOnPosition(this.transform.position);

        _gridData.SetTileUsed(tmpIndex.x, tmpIndex.y);
        foreach (Vector2Int element in building.tilesCoordinate)
        {   
            _gridData.SetTileUsed(tmpIndex.x + element.x, tmpIndex.y + element.y);
        }
      // Adrien
      //_BuildingList.AppendToBuildingList(building.BuildingData);
    }

    bool isFlamePropagation = false;
    private void Update()
    {
        if(this.transform.position.y > 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 10f * Time.deltaTime, this.transform.position.z);
        }
        else if (!isFlamePropagation)
        {
            //StartCoroutine(FlamePropagation());
            isFlamePropagation =true;
        }
    }

    public void SpawnWonder()
    {
        GameObject wonder = GameObject.Instantiate(wonderBuilding, new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.identity);
        S_Building tmpWonder = wonder.GetComponent<S_Building>();
        tmpWonder.PlacedBuilding();
        tmpWonder.isPlacedAnimation = true;
        // Adrien
        _BuildingList.AppendToBuildingList(tmpWonder.BuildingData);
    }

    void FlamePropagation()
    {
        isFirePropagating = true;

        Vector3 tmpCoordinate = _gridData.GetRandomTileAroundOtherOne(_gridData.GetIndexbasedOnPosition(this.transform.position), 3, true);
        if(tmpCoordinate != Vector3.zero)
        {
            if (!_gridData.gridsUsageStatement[_gridData.GetIndexbasedOnPosition(tmpCoordinate).x][_gridData.GetIndexbasedOnPosition(tmpCoordinate).y].statement)
            {
                allFire.Add(GameObject.Instantiate(smallFire, tmpCoordinate, Quaternion.identity));

                Vector2Int tmpIndex = _gridData.GetIndexbasedOnPosition(tmpCoordinate);
                gridUsage.Add(tmpIndex);
                _gridData.SetTileUsed(tmpIndex.x, tmpIndex.y);
            }

        }


    }
    public void Clear()
    {
        foreach (Vector2Int element in gridUsage)
        {
            _gridData.RemoveTileUsed(element.x, element.y);
        }
        _scriptableRounds.OnActionPointRemoved -= FlamePropagation;
        foreach (GameObject Object in allFire)
        {
            Destroy(Object);
        }

        Destroy(gameObject);
    }

    //IEnumerator FlamePropagation()
    //{
    //    isFirePropagating = true;
    //    yield return new WaitForSeconds(delayBeforeFlamePropagation);
    //    while (isFirePropagating)
    //    {
    //        yield return new WaitForSeconds(1f);

    //        Vector3 tmpCoordinate = _gridData.GetRandomTileAroundOtherOne(_gridData.GetIndexbasedOnPosition(this.transform.position), 3, true);
    //        if (tmpCoordinate != Vector3.zero)
    //        {
    //            if (!_gridData.gridsUsageStatement[_gridData.GetIndexbasedOnPosition(tmpCoordinate).x][_gridData.GetIndexbasedOnPosition(tmpCoordinate).y].statement)
    //            {
    //                allFire.Add(GameObject.Instantiate(smallFire, tmpCoordinate, Quaternion.identity));

    //                Vector2Int tmpIndex = _gridData.GetIndexbasedOnPosition(tmpCoordinate);
    //                gridUsage.Add(tmpIndex);
    //                _gridData.SetTileUsed(tmpIndex.x, tmpIndex.y);
    //            }

    //        }

    //    }
    //}
}
