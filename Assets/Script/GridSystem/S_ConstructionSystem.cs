using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ConstructionSystem : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject objectToSpawn2;
    //private bool isObjectPlaced = false;
    GameObject objectSpawned = null;

    public S_Currencies joyCurrency, angerCurrency, sadCurrency, fearCurrency, consciousTreeToken;
    public S_FeelsUI feelsUI;

    public List<int> TierLimitInPool = new List<int>();
    public List<GameObject> AllBuildings, BuildingInPool = new List<GameObject>();
    public List<List<GameObject>> buildingPerTier = new List<List<GameObject>>();

    public delegate void RefreshBuildingPoolDelegate();
    public event RefreshBuildingPoolDelegate OnRefreshBuildingPool;

    Vector3 lastCursorPosition;

    private void Start()
    {
        StoreBuildingPerTier();
        RefreshBuildingPool();
    }

    
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
                objectSpawned.GetComponent<S_Building>().SetDestination(Grid.ClampPositionToGrid(hit.point));
               
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
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            if(objectSpawned != null )
            {
                PlaceBuilding();
            }
        }
    }

    void StoreBuildingPerTier()
    {
        for(int i = 0; i < TierLimitInPool.Count; i++)
        {
            buildingPerTier.Add(new List<GameObject>());
        }
        //Sort
        foreach (var build in AllBuildings)
        {
            buildingPerTier[build.GetComponent<S_Building>().tier].Add(build);
        }
    }
    public void RefreshBuildingPool()
    {
        BuildingInPool.Clear();
        List<List<GameObject>> tmpBuildingPerTier = new List<List<GameObject>>();

        //Initiate list
        for (int i = 0; i < TierLimitInPool.Count; i++)
        {
            tmpBuildingPerTier.Add(new List<GameObject>());
        }

        //Add element base on probability
        for (int i = 1; i < buildingPerTier.Count; i++)
        {
            for(int j = 0; j < buildingPerTier[i].Count; j++)
            {
                if (Random.Range(0, 101) < buildingPerTier[i][j].GetComponent<S_Building>().probabilityToSpawnInPool)
                    tmpBuildingPerTier[i].Add(buildingPerTier[i][j]);
            }
        }
        //Add element base on limitation
        for (int i = 1; i < TierLimitInPool.Count; ++i)
        {
            S_GameFunction.Shuffle<GameObject>(tmpBuildingPerTier[i]);

            for (int j = 0; j < TierLimitInPool[i]; j++)
                if (tmpBuildingPerTier[i].Count > 0)
                    BuildingInPool.Add(tmpBuildingPerTier[i][j]);
        }

        //Fill with tier 0
        if(BuildingInPool.Count < 8)
        {
            int index = 0;
            S_GameFunction.Shuffle<GameObject>(buildingPerTier[0]);
            for (int i = BuildingInPool.Count; i < 8; i++)
            {
                BuildingInPool.Add(buildingPerTier[0][index]);
                index++;
            }
        }
        S_GameFunction.Shuffle<GameObject>(BuildingInPool);

        OnRefreshBuildingPool.Invoke();
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

        //Check if enough Money
        if(objectSpawnedBuildingScript.FeelType && objectSpawnedBuildingScript.FeelType.amount - objectSpawnedBuildingScript.price < 0)
            canPlaceBuilding = false;

        if (canPlaceBuilding)
        {
            for (int i = 0; i < objectSpawnTilesUsage.Count; i++)
            {
                Grid.gridsUsageStatement[tmpIndexInGrid.x + objectSpawnTilesUsage[i].x][tmpIndexInGrid.y - objectSpawnTilesUsage[i].y] = true;
            }


            feelsUI.RefreshUI();
            if(objectSpawnedBuildingScript.FeelType)
                objectSpawnedBuildingScript.FeelType.RemoveAmount(objectSpawnedBuildingScript.price);

            consciousTreeToken.amount += 1;

            objectSpawned.GetComponent<S_Building>().PlacedBuilding();
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

    public void SpawnObject(GameObject gameObject)
    {
        if(objectSpawned != null)
            Destroy(objectSpawned);

        objectSpawned = SpawnGameObject(Vector3.zero, gameObject);
    }

    GameObject SpawnGameObject(Vector3 spawnPoint, GameObject gameObject = null)
    {
        Vector3 spawnPoinTtmp = Vector3.zero;
        if (objectToSpawn != null && spawnPoinTtmp != null)
        {
            GameObject tmp = Instantiate(gameObject, spawnPoinTtmp, Quaternion.identity);
            return tmp;

        }
        else
        {
            UnityEngine.Debug.LogError("Object to spawn or spawn point not set!");
            return null;
        }

    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
