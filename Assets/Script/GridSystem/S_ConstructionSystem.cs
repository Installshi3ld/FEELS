using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstructionSystem : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject objectToSpawn2;
    private bool isObjectPlaced = false;
    GameObject objectSpawned = null;

    public S_Currencies joyCurrency, angerCurrency, sadCurrency, fearCurrency, consciousTreeToken;
    public S_FeelsUI feelsUI;

    Vector3 lastCursorPosition;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Mouse raycast
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

            if(objectSpawned != null && Grid.ClampPositionToGrid(hit.point) != lastCursorPosition)
            {
                StartCoroutine(objectSpawned.GetComponent<S_Building>().SmoothObjectPositionBetweenVector(Grid.ClampPositionToGrid(hit.point)));
                lastCursorPosition = Grid.ClampPositionToGrid(hit.point);
            }

            UnityEngine.Debug.DrawRay(hit.point, hit.normal, Color.blue);

        }


        //Spawn object
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectSpawned == null) {
                objectSpawned = SpawnGameObject(Vector3.zero, objectToSpawn);
            }
            else if (objectSpawned != null)
            {
                Destroy(objectSpawned);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (objectSpawned == null)
            {
                objectSpawned = SpawnGameObject(Vector3.zero, objectToSpawn2);
            }
            else if (objectSpawned != null)
            {
                Destroy(objectSpawned);
            }
        }

        //Place Object
        if (Input.GetMouseButtonDown(0))
        {
            if(objectSpawned != null )
            {
                PlaceBuilding();
            }
        }
    }

    void PlaceBuilding()
    {
        S_Building objectSpawnedBuildingScript = objectSpawned.GetComponent<S_Building>();
        List<Vector2Int> objectSpawnTilesUsage = objectSpawnedBuildingScript.tilesCoordinate;
       
        Vector2Int tmpIndexInGrid = GetObjectIndexInGridUsage(objectSpawned);
        bool canPlaceBuilding = true;

        //Check if Index are on non used tile
        for (int i = 0; i < objectSpawnTilesUsage.Count; i++)
        {
            //If building is outside 2 dimension list
            if (tmpIndexInGrid.x + objectSpawnTilesUsage[i].x >= Grid.gridsUsageStatement.Count ||
                tmpIndexInGrid.y + objectSpawnTilesUsage[i].y >= Grid.gridsUsageStatement.Count ||
                tmpIndexInGrid.y + objectSpawnTilesUsage[i].y < 0 ||
                tmpIndexInGrid.x + objectSpawnTilesUsage[i].x < 0)
            {
                canPlaceBuilding = false;
                break;
            }

            //1 -> Tile used 2 -> If outside mapSphereArea 
            if (Grid.gridsUsageStatement[tmpIndexInGrid.x + objectSpawnTilesUsage[i].x][tmpIndexInGrid.y - objectSpawnTilesUsage[i].y] 
                ||
                Grid.fogGridsUsageStatement[tmpIndexInGrid.x + objectSpawnTilesUsage[i].x][tmpIndexInGrid.y - objectSpawnTilesUsage[i].y])
            {
                canPlaceBuilding = false;
            }

        }

        List<int> tmpAmountToRemove = new List<int> { 0, 0, 0, 0 };
        //Check if enough feel
        for(int i = 0; i < objectSpawnedBuildingScript.feelsCostList.Count; i++)
        {
            switch (objectSpawnedBuildingScript.feelsCostList[i].feelsType)
            {
                case S_Building.FeelsType.Joy:
                    if (!(joyCurrency.amount - objectSpawnedBuildingScript.feelsCostList[i].cost >= 0))
                        canPlaceBuilding = false;

                    tmpAmountToRemove[0] = objectSpawnedBuildingScript.feelsCostList[i].cost;
                    break;

                case S_Building.FeelsType.Anger:
                    if (!(angerCurrency.amount - objectSpawnedBuildingScript.feelsCostList[i].cost >= 0))
                        canPlaceBuilding = false;
                    tmpAmountToRemove[1] = objectSpawnedBuildingScript.feelsCostList[i].cost;
                    break;

                case S_Building.FeelsType.Sad:
                    if (!(sadCurrency.amount - objectSpawnedBuildingScript.feelsCostList[i].cost >= 0))
                        canPlaceBuilding = false;
                    tmpAmountToRemove[2] = objectSpawnedBuildingScript.feelsCostList[i].cost;
                    break;

                case S_Building.FeelsType.Fear:
                    if (!(fearCurrency.amount - objectSpawnedBuildingScript.feelsCostList[i].cost >= 0))
                        canPlaceBuilding = false;
                    tmpAmountToRemove[3] = objectSpawnedBuildingScript.feelsCostList[i].cost;
                    break;
            }
        }

        if (canPlaceBuilding)
        {
            for (int i = 0; i < objectSpawnTilesUsage.Count; i++)
            {
                Grid.gridsUsageStatement[tmpIndexInGrid.x + objectSpawnTilesUsage[i].x][tmpIndexInGrid.y - objectSpawnTilesUsage[i].y] = true;
            }

            joyCurrency.amount -= tmpAmountToRemove[0];
            angerCurrency.amount -= tmpAmountToRemove[1];
            sadCurrency.amount -= tmpAmountToRemove[2];
            fearCurrency.amount -= tmpAmountToRemove[3];
            feelsUI.RefreshUI();

            consciousTreeToken.amount += 1;

            objectSpawned = null;
        }
        else
            feelsUI.Info("Need more feels");
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


    GameObject SpawnGameObject(Vector3 spawnPoint, GameObject gameObject = null)
    {
        if (objectToSpawn != null && spawnPoint != null)
        {
            GameObject tmp = Instantiate(gameObject, spawnPoint, Quaternion.identity);
            return tmp;

        }
        else
        {
            UnityEngine.Debug.LogError("Object to spawn or spawn point not set!");
            return null;
        }

    }
}
