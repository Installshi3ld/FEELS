using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;

public class S_LifeExperience : MonoBehaviour
{

    public GameObject smallFire;

    public GameObject wonderBuilding;

    List<GameObject> allFire = new List<GameObject>();
    List<Vector2Int> gridUsage = new List<Vector2Int>();
    private void Awake()
    {
        wonderBuilding.GetComponent<S_Building>().isPlaced = true;
    }
    private void Start()
    {
        S_Building building = wonderBuilding.GetComponent<S_Building>();
        this.transform.position = Grid.GetRandomTileInGrid(building.tilesCoordinate) + new Vector3(0, 50, 0);

        if (this.transform.position.y <= -500)
            Destroy(this);

        
        Vector2Int tmpIndex = Grid.getIndexbasedOnPosition(this.transform.position);

        Grid.SetTileUsed(tmpIndex.x, tmpIndex.y);
        foreach (Vector2Int element in building.tilesCoordinate)
        {
            Grid.SetTileUsed(tmpIndex.x + element.x, tmpIndex.y + element.y);
        }
        
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
            StartCoroutine(FlamePropagation());
            isFlamePropagation =true;
        }
    }

    void SpawnWonder()
    {
        GameObject.Instantiate(wonderBuilding, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator FlamePropagation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector3 tmpCoordinate = Grid.GetRandomTileAroundOtherOne(Grid.getIndexbasedOnPosition(this.transform.position), 3);
            if (!Grid.gridsUsageStatement[Grid.getIndexbasedOnPosition(tmpCoordinate).x][Grid.getIndexbasedOnPosition(tmpCoordinate).y])
            {
                allFire.Add(GameObject.Instantiate(smallFire, tmpCoordinate, Quaternion.identity));

                Vector2Int tmpIndex = Grid.getIndexbasedOnPosition(tmpCoordinate);
                gridUsage.Add(tmpIndex);
                Grid.SetTileUsed(tmpIndex.x, tmpIndex.y);
            }
            
        }
        
    }
    private void OnDisable()
    {
        foreach (Vector2Int element in gridUsage)
        {
            Grid.RemoveTileUsed(element.x, element.y);
        }
        foreach (GameObject Object in allFire)
        {
            Destroy(Object);
        }

        SpawnWonder();
    }
}
