using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class ConstructionSystem : MonoBehaviour
{
    [SerializeField]private S_GridData _gridData;

    public GameObject objectToSpawn;
    GameObject objectSpawned = null;

    public S_Currencies joyCurrency, angerCurrency, sadCurrency, fearCurrency, consciousTreeToken;
    public S_FeelsUI feelsUI;

    public List<int> TierLimitInPool = new List<int>();
    public List<GameObject> AllBuildings = new List<GameObject>();
    [NonSerialized]
    public List<GameObject> BuildingInPool = new List<GameObject>();
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

            if(objectSpawned != null && _gridData.ClampPositionToGrid(hit.point) != lastCursorPosition)
            {
                objectSpawned.GetComponent<S_Building>().SetDestination(_gridData.ClampPositionToGrid(hit.point));
               
                lastCursorPosition = _gridData.ClampPositionToGrid(hit.point);
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
                if (UnityEngine.Random.Range(0, 101) < buildingPerTier[i][j].GetComponent<S_Building>().probabilityToSpawnInPool)
                    tmpBuildingPerTier[i].Add(buildingPerTier[i][j]);
            }
        }
        //Add element base on limitation
        for (int i = 1; i < TierLimitInPool.Count; ++i)
        {
            S_StaticFunc.Shuffle<GameObject>(tmpBuildingPerTier[i]);

            for (int j = 0; j < TierLimitInPool[i]; j++)
                if (tmpBuildingPerTier[i].Count > 0)
                    BuildingInPool.Add(tmpBuildingPerTier[i][j]);
        }

        //Fill with tier 0
        if(BuildingInPool.Count < 8)
        {
            int index = 0;
            S_StaticFunc.Shuffle<GameObject>(buildingPerTier[0]);
            for (int i = BuildingInPool.Count; i < 8; i++)
            {
                BuildingInPool.Add(buildingPerTier[0][index]);
                index++;
            }
        }
        S_StaticFunc.Shuffle<GameObject>(BuildingInPool);

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
            if (tmpIndexInGrid.x + objectSpawnTilesUsage[i].x >= _gridData.gridsUsageStatement.Count ||
                tmpIndexInGrid.y + objectSpawnTilesUsage[i].y >= _gridData.gridsUsageStatement.Count ||
                tmpIndexInGrid.y + objectSpawnTilesUsage[i].y < 0 ||
                tmpIndexInGrid.x + objectSpawnTilesUsage[i].x < 0)
            {
                canPlaceBuilding = false;
                break;
            }

            //1 -> Tile used 2 -> If outside mapSphereArea 
            if (_gridData.gridsUsageStatement[tmpIndexInGrid.x + objectSpawnTilesUsage[i].x][tmpIndexInGrid.y - objectSpawnTilesUsage[i].y].statement 
                ||
                Grid.fogGridsUsageStatement[tmpIndexInGrid.x + objectSpawnTilesUsage[i].x][tmpIndexInGrid.y - objectSpawnTilesUsage[i].y])
            {
                canPlaceBuilding = false;
            }

        }

        //Check if enough Money
        if(objectSpawnedBuildingScript.FeelCurrency && objectSpawnedBuildingScript.FeelCurrency.amount - objectSpawnedBuildingScript.price < 0)
            canPlaceBuilding = false;

        if (canPlaceBuilding)
        {
            for (int i = 0; i < objectSpawnTilesUsage.Count; i++)
            {
                int x = tmpIndexInGrid.x + objectSpawnTilesUsage[i].x;
                int y = tmpIndexInGrid.y - objectSpawnTilesUsage[i].y;
                _gridData.gridsUsageStatement[x][y].statement = true;
                _gridData.gridsUsageStatement[x][y].building = objectSpawned;
                
            }


            feelsUI.RefreshUI();
            if(objectSpawnedBuildingScript.FeelCurrency)
                objectSpawnedBuildingScript.FeelCurrency.RemoveAmount(objectSpawnedBuildingScript.price);

            consciousTreeToken.AddAmount(1);

            CheckBoostBuilding();
            objectSpawned.GetComponent<S_Building>().PlacedBuilding();

            objectSpawned = null;
        }
        else
            feelsUI.Info("Need more feels");
    }


    Vector2Int GetObjectIndexInGridUsage(GameObject objectSpawned)
    {
        //Get index base in gridUsageStatement based on position
        int indexX = (int)objectSpawned.transform.position.x / _gridData.tileSize + _gridData.gridsUsageStatement.Count / 2;
        int indexZ = (int)objectSpawned.transform.position.z / _gridData.tileSize + _gridData.gridsUsageStatement.Count / 2;

        return new Vector2Int(indexX, indexZ);
    }

    bool GetTileStatementWithIndex(Vector2Int positionOnGrid)
    {
        float positionX = (float)positionOnGrid.x * _gridData.tileSize - _gridData.gridsUsageStatement.Count * 2;

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
    void CheckBoostBuilding()
    {
        List<Vector2Int> _tilesToCheckForBoost = new List<Vector2Int>();
        S_Building s_building = objectSpawned.GetComponent<S_Building>();
        _tilesToCheckForBoost = s_building.GetSurroundingTiles();

        S_Currencies.FeelType _feelType = S_Currencies.FeelType.None;
        if (s_building.FeelCurrency)
            _feelType = s_building.FeelCurrency.feelType;

        GameObject _currentBuildingToCheck = null;
        Vector2Int buildingCoordinate = GetObjectIndexInGridUsage(objectSpawned);

        //Check tile for boost
        for (int i = 0; i < _tilesToCheckForBoost.Count; i++)
        {

            _currentBuildingToCheck = _gridData.gridsUsageStatement[buildingCoordinate.x + _tilesToCheckForBoost[i].x][buildingCoordinate.y - _tilesToCheckForBoost[i].y].building;

            // Apply behavior of case to boost bellow
            S_Currencies.FeelType _currentBuildingToCheckFeelType;
            if (_currentBuildingToCheck && _currentBuildingToCheck.GetComponent<S_Building>() && _currentBuildingToCheck.GetComponent<S_FeelAssignationBuilding>())
            {
                _currentBuildingToCheckFeelType = _currentBuildingToCheck.GetComponent<S_Building>().FeelCurrency.feelType;

                switch (_feelType)
                {
                    case S_Currencies.FeelType.Joy:
                        if (_feelType == _currentBuildingToCheckFeelType)
                        {
                            _currentBuildingToCheck.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
                            objectSpawned.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
                        }
                        break;

                    case S_Currencies.FeelType.Anger:
                        if (_feelType == _currentBuildingToCheckFeelType)
                        {
                            _currentBuildingToCheck.GetComponent<S_FeelAssignationBuilding>().UnBoostBuilding();
                            objectSpawned.GetComponent<S_FeelAssignationBuilding>().UnBoostBuilding();
                        }
                        break;

                    case S_Currencies.FeelType.Fear:

                        if (_feelType != _currentBuildingToCheckFeelType)
                        {
                            objectSpawned.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
                        }
                        if (_feelType == _currentBuildingToCheckFeelType)
                        {
                            _currentBuildingToCheck.GetComponent<S_FeelAssignationBuilding>().UnBoostBuilding();
                            objectSpawned.GetComponent<S_FeelAssignationBuilding>().UnBoostBuilding();
                        }

                        break;
                }
            }
        }

        if(_feelType == S_Currencies.FeelType.Sad)
        {
            List<GameObject> buildingSadToBoost = new List<GameObject>();

            List<Vector2Int> corners = s_building.GetCornerTiles();

            for (int i = 0; i < corners.Count; i++)
            {
                _currentBuildingToCheck = _gridData.gridsUsageStatement[buildingCoordinate.x + corners[i].x][buildingCoordinate.y - corners[i].y].building;
                if (_currentBuildingToCheck && _currentBuildingToCheck.GetComponent<S_Building>())
                {
                    S_Currencies.FeelType _currentBuildingToCheckFeelType = _currentBuildingToCheck.GetComponent<S_Building>().FeelCurrency.feelType;

                    if (_feelType == _currentBuildingToCheckFeelType && !buildingSadToBoost.Contains(_currentBuildingToCheck))
                    {
                        buildingSadToBoost.Add(_currentBuildingToCheck);
                    }
                }
            }
        
            for (int i = 0; i < _tilesToCheckForBoost.Count; i++)
            {
                _currentBuildingToCheck = _gridData.gridsUsageStatement[buildingCoordinate.x + _tilesToCheckForBoost[i].x][buildingCoordinate.y - _tilesToCheckForBoost[i].y].building;
                if (_currentBuildingToCheck && _currentBuildingToCheck.GetComponent<S_Building>())
                {
                    S_Currencies.FeelType _currentBuildingToCheckFeelType = _currentBuildingToCheck.GetComponent<S_Building>().FeelCurrency.feelType;

                    if (_feelType == _currentBuildingToCheckFeelType && buildingSadToBoost.Contains(_currentBuildingToCheck))
                    {
                        buildingSadToBoost.Remove(_currentBuildingToCheck);
                    }

                }
            }

            foreach (GameObject sadBuild in buildingSadToBoost)
            {
                if (sadBuild.GetComponent<S_FeelAssignationBuilding>()) { }
                    sadBuild.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
            }

            if(buildingSadToBoost.Count > 0)
            {
                if(objectSpawned.GetComponent<S_FeelAssignationBuilding>())
                    objectSpawned.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
            }
        }

    }

}
