using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstructionSystem : MonoBehaviour
{
    public GameObject objectToSpawn;
    private bool isObjectPlaced = false;
    GameObject objectSpawned = null;



    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Mouse raycast
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Vector3 positionDuHit = hit.point;
            
            UnityEngine.Debug.DrawRay(hit.point, hit.normal, Color.blue);

        }


        //Spawn object
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectSpawned == null) {
                objectSpawned = SpawnGameObject(Vector3.zero);
            }
            
            
        }
        
        //Move object
        if (objectSpawned != null)
        {
            if(Vector3.Distance(new Vector3(0,0,0), hit.point) <= Grid.mapSphereArea - Grid.padding)
                objectSpawned.transform.position = objectSpawned.GetComponent<S_Building>().ClampPositionToGrid(hit.point);

        }

        //Place Object
        if (Input.GetMouseButtonDown(0))
        {
            if(objectSpawned != null)
            {
                PlaceBuilding();
            }
        }
    }

    void PlaceBuilding()
    {
        List<Vector2Int> objectSpawnTilesUsage = objectSpawned.GetComponent<S_Building>().tilesCoordinate;

        Vector2Int tmpIndexInGrid = GetObjectIndexInGridUsage(objectSpawned);
        bool canPlaceBuilding = true;

        //Check if Index are on non used tile
        for (int i = 0; i < objectSpawnTilesUsage.Count; i++)
        {
            //if all tile available
            if (Grid.gridsUsageStatement[tmpIndexInGrid.x + objectSpawnTilesUsage[i].x][tmpIndexInGrid.y - objectSpawnTilesUsage[i].y])
            {
                canPlaceBuilding = false;
            }
        }

        if (canPlaceBuilding)
        {
            for (int i = 0; i < objectSpawnTilesUsage.Count; i++)
            {
                Grid.gridsUsageStatement[tmpIndexInGrid.x + objectSpawnTilesUsage[i].x][tmpIndexInGrid.y - objectSpawnTilesUsage[i].y] = true;
            }
            objectSpawned = null;
        }
        
    }

    Vector2Int GetObjectIndexInGridUsage(GameObject objectSpawned)
    {
        //Get index base in gridUsageStatement based on position
        int indexX = (int)objectSpawned.transform.position.x / Grid.tileSize + Grid.gridsUsageStatement.Count / 2;
        int indexZ = (int)objectSpawned.transform.position.z / Grid.tileSize + Grid.gridsUsageStatement.Count / 2;

        return new Vector2Int(indexX, indexZ);
    }

    bool GetTileStatementWithIndex(Vector2Int positionOnGrid)
    {
        float positionX = (float)positionOnGrid.x * Grid.tileSize - Grid.gridsUsageStatement.Count * 2;

        return true;
    }


    GameObject SpawnGameObject(Vector3 spawnPoint)
    {
        if (objectToSpawn != null && spawnPoint != null)
        {
            GameObject tmp = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity);
            return tmp;

        }
        else
        {
            UnityEngine.Debug.LogError("Object to spawn or spawn point not set!");
            return null;
        }

    }
}
