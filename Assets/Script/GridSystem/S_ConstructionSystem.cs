using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static S_Currencies;


public class ConstructionSystem : MonoBehaviour
{
    [SerializeField] private S_BuildingList buildingListContainer;

    [SerializeField]private S_GridData _gridData;

    public GameObject objectToSpawn;
    GameObject objectSpawned = null;

    public S_Currencies joyCurrency, angerCurrency, sadCurrency, fearCurrency, consciousTreeToken;
    public S_FeelsUI feelsUI;

    Vector3 lastCursorPosition;
    private void OnDestroy()
    {
        buildingListContainer.ResetOnDestroy();
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
        if (Input.GetMouseButtonDown(0) && !S_StaticFunc.IsMouseOverUI())
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

        if (!IsValidPlacement(tmpIndexInGrid, objectSpawnTilesUsage) || !HasEnoughMoney(objectSpawnedBuildingScript))
        {
            feelsUI.Info("Need more feels");
            return;
        }

        UpdateGridOnPlacement(tmpIndexInGrid, objectSpawnTilesUsage, objectSpawnedBuildingScript);

        feelsUI.RefreshUI();

        if (objectSpawnedBuildingScript.BuildingData.feelTypeCostList[0].feelTypeCurrency)
            objectSpawnedBuildingScript.RemoveFeelCost();

        consciousTreeToken.AddAmount(1);

        CheckBoostBuilding();
        objectSpawnedBuildingScript.PlacedBuilding();

        buildingListContainer.AppendToBuildingList(objectSpawnedBuildingScript.BuildingData);

        objectSpawned = null;
    }

    bool IsValidPlacement(Vector2Int tmpIndexInGrid, List<Vector2Int> objectSpawnTilesUsage)
    {
        for (int i = 0; i < objectSpawnTilesUsage.Count; i++)
        {
            int x = tmpIndexInGrid.x + objectSpawnTilesUsage[i].x;
            int y = tmpIndexInGrid.y - objectSpawnTilesUsage[i].y;

            if (!IsWithinGridBounds(x, y) || IsTileOccupied(x, y))
            {
                return false;
            }
        }
        return true;
    }

    bool IsWithinGridBounds(int x, int y)
    {
        int gridSize = _gridData.gridsUsageStatement.Count;
        return x >= 0 && x < gridSize && y >= 0 && y < gridSize;
    }

    bool IsTileOccupied(int x, int y)
    {
        return _gridData.gridsUsageStatement[x][y].statement || Grid.fogGridsUsageStatement[x][y];
    }

    bool HasEnoughMoney(S_Building buildingScript)
    {
        return buildingScript.HasEnoughMoney();
    }

    void UpdateGridOnPlacement(Vector2Int tmpIndexInGrid, List<Vector2Int> objectSpawnTilesUsage, S_Building buildingScript)
    {
        for (int i = 0; i < objectSpawnTilesUsage.Count; i++)
        {
            int x = tmpIndexInGrid.x + objectSpawnTilesUsage[i].x;
            int y = tmpIndexInGrid.y - objectSpawnTilesUsage[i].y;

            _gridData.gridsUsageStatement[x][y].statement = true;
            _gridData.gridsUsageStatement[x][y].building = objectSpawned;
        }
    }



    Vector2Int GetObjectIndexInGridUsage(GameObject objectSpawned)
    {
        //Get index base in gridUsageStatement based on position
        int indexX = (int)objectSpawned.transform.position.x / _gridData.tileSize + _gridData.gridsUsageStatement.Count / 2;
        int indexZ = (int)objectSpawned.transform.position.z / _gridData.tileSize + _gridData.gridsUsageStatement.Count / 2;

        return new Vector2Int(indexX, indexZ);
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
    void CheckBoostBuilding()
    {
        S_Building _building = objectSpawned.GetComponent<S_Building>();
        List<Vector2Int> _tilesToCheckForBoost = _building.GetSurroundingTiles();

        FeelType _feelType = FeelType.None;
        if (_building)
            _feelType = _building.BuildingData.feelType;

        Vector2Int _buildingCoordinate = GetObjectIndexInGridUsage(objectSpawned);

        //Check tile for boost
        for (int i = 0; i < _tilesToCheckForBoost.Count; i++)
        {

            GameObject _currentBuildingToCheck = _gridData.gridsUsageStatement[_buildingCoordinate.x + _tilesToCheckForBoost[i].x][_buildingCoordinate.y - _tilesToCheckForBoost[i].y].building;

            // Apply behavior of case to boost bellow
            CheckTileAndBoost(_currentBuildingToCheck, _feelType);

            if (_feelType == FeelType.Sad)
                CheckTileSadAndBoost(_currentBuildingToCheck, _buildingCoordinate, _building, _tilesToCheckForBoost);
        }


        

    }

    private void CheckTileSadAndBoost(GameObject _currentBuildingToCheck, Vector2Int _buildingCoordinate, S_Building _building, List<Vector2Int> _tilesToCheckForBoost)
    {
        List<GameObject> buildingSadToBoost = new List<GameObject>();

        List<Vector2Int> corners = _building.GetCornerTiles();

        for (int i = 0; i < corners.Count; i++)
        {
            _currentBuildingToCheck = _gridData.gridsUsageStatement[_buildingCoordinate.x + corners[i].x][_buildingCoordinate.y - corners[i].y].building;
            if (_currentBuildingToCheck && _currentBuildingToCheck.GetComponent<S_Building>())
            {
                FeelType _currentBuildingToCheckFeelType = _currentBuildingToCheck.GetComponent<S_Building>().BuildingData.feelType;

                if (FeelType.Sad == _currentBuildingToCheckFeelType && !buildingSadToBoost.Contains(_currentBuildingToCheck))
                {
                    buildingSadToBoost.Add(_currentBuildingToCheck);
                }
            }
        }

        for (int i = 0; i < _tilesToCheckForBoost.Count; i++)
        {
            _currentBuildingToCheck = _gridData.gridsUsageStatement[_buildingCoordinate.x + _tilesToCheckForBoost[i].x][_buildingCoordinate.y - _tilesToCheckForBoost[i].y].building;
            if (_currentBuildingToCheck && _currentBuildingToCheck.GetComponent<S_Building>())
            {
                FeelType _currentBuildingToCheckFeelType = _currentBuildingToCheck.GetComponent<S_Building>().BuildingData.feelType;

                if (FeelType.Sad == _currentBuildingToCheckFeelType && buildingSadToBoost.Contains(_currentBuildingToCheck))
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

        if (buildingSadToBoost.Count > 0)
        {
            if (objectSpawned.GetComponent<S_FeelAssignationBuilding>())
                objectSpawned.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
        }
    }



    private void CheckTileAndBoost(GameObject _currentBuildingToCheck, FeelType _feelType)
    {
        FeelType _currentBuildingToCheckFeelType;

        if (_currentBuildingToCheck && _currentBuildingToCheck.GetComponent<S_Building>() && _currentBuildingToCheck.GetComponent<S_FeelAssignationBuilding>())
        {
            _currentBuildingToCheckFeelType = _currentBuildingToCheck.GetComponent<S_Building>().BuildingData.feelType;

            switch (_feelType)
            {
                case FeelType.Joy:
                    if (_feelType == _currentBuildingToCheckFeelType)
                    {
                        _currentBuildingToCheck.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
                        objectSpawned.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
                    }
                    break;

                case FeelType.Anger:
                    if (_feelType == _currentBuildingToCheckFeelType)
                    {
                        _currentBuildingToCheck.GetComponent<S_FeelAssignationBuilding>().UnBoostBuilding();
                        objectSpawned.GetComponent<S_FeelAssignationBuilding>().UnBoostBuilding();
                    }
                    break;
                case FeelType.Fear:

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

}
