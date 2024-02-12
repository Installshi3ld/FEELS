using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConstructionSystem : MonoBehaviour
{
    [Header("General Data")]
    [SerializeField] private S_BuildingList buildingListContainer;
    [SerializeField]private S_GridData _gridData;
    [SerializeField] private S_FogData _fogData;
    [SerializeField] private S_ScriptableRounds ScriptableRounds;
    [SerializeField] private S_MenuData _isBuilding;

    public GameObject objectToSpawn;
    public GameObject planePlacementValid;

    [Header("Currencies Data")]
    public S_Currencies joyCurrency;
    public S_Currencies angerCurrency, sadCurrency, fearCurrency, consciousTreeToken;

    [Header("Material")]
    [SerializeField]  private Material _placementValid;
    [SerializeField]  private Material _placementNotValid;
    private Material _buildingMaterial;


    [Space]
    public S_FeelsUI feelsUI;
    private GameObject planePlacement;

    GameObject objectSpawned = null;
    Vector3 lastCursorPosition;
    GameObject _planePlacementValid;

    private void Start()
    {
        _planePlacementValid = Instantiate(planePlacementValid);
        HidePlanePlacement();
    }

    private void OnDestroy()
    {
        buildingListContainer.ResetOnDestroy();
    }

    int joyPlaced=0, angerPlaced=0, sadPlaced=0, fearPlaced=0;

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
                S_Building _building = objectSpawned.GetComponent<S_Building>();

                _building.SetDestination(_gridData.ClampPositionToGrid(hit.point));
                lastCursorPosition = _gridData.ClampPositionToGrid(hit.point);

                ChangePlanePlacementUnderBuilding(_building);


                //Feedback for tile which will boost building
                foreach (S_BuildingData build in buildingListContainer.builidingsInfos)
                {
                    if(_building.BuildingData.feelType == build.feelType && build.feelType == FeelType.Joy && joyPlaced < 2)
                    {
                        EnableFeedBuildingTile(build, build.building.GetSurroundingTiles());

                    }

                    //Sad 
                    if (_building.BuildingData.feelType == build.feelType &&  build.feelType == FeelType.Sad && sadPlaced < 2)
                    {
                        EnableFeedBuildingTile(build, build.building.GetCornerTiles());

                    }

                    //Anger 
                    if (_building.BuildingData.feelType == build.feelType && build.feelType == FeelType.Anger && angerPlaced < 2)
                    {
                        _gridData.SetAllPlaneFeedbackBuildingEnable();

                        EnableFeedBuildingTile(build, build.building.GetSurroundingTiles(), false);
                    }
                    //Fear
                    if (_building.BuildingData.feelType == FeelType.Fear && build.feelType != FeelType.Fear && fearPlaced < 2)
                    {
                        EnableFeedBuildingTile(build, build.building.GetSurroundingTiles());

                        //Remove tile around fear building
                        foreach (S_BuildingData tmpbuild in buildingListContainer.builidingsInfos)
                        {
                            if(tmpbuild.feelType == FeelType.Fear)
                            {
                                EnableFeedBuildingTile(tmpbuild, tmpbuild.building.GetSurroundingTiles(), false);
                            }

                        }
                    }
                }
            }
        }
    }

    
    private void LateUpdate()
    {
        //Spawn object
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (objectSpawned != null)
            {
                DestroyObjectSpawned();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            DestroyObjectSpawned();
        }

        //Place Object
        if (Input.GetMouseButtonDown(0) && !S_StaticFunc.IsMouseOverUI())
        {
            if (objectSpawned != null)
            {
                PlaceBuilding();
            }
        }
    }

    void DestroyObjectSpawned()
    {
        Destroy(objectSpawned);
        HidePlanePlacement();
        _gridData.ClearPlaneFeedbackBuildingStatement();
        _isBuilding.value = false;
    }

    //Use for the tutorial
    void EnableFeedBuildingTile(S_BuildingData build, List<Vector2Int> tile, bool statement = true)
    {
        foreach (Vector2Int coord in tile)
        {
            //Calculate Vector3 Global Coord
            Vector3 tmpVect = build.building.destination;
            tmpVect.x = tmpVect.x + coord.x * _gridData.tileSize;
            tmpVect.z = tmpVect.z - coord.y * _gridData.tileSize;

            Vector2Int tmpCoord = _gridData.GetIndexbasedOnPosition(tmpVect);

            _gridData.SetPlaneFeedbackBuildingStatement(tmpCoord.x, tmpCoord.y, statement);
        }
    }
    List<Vector2Int> GetObjectSpawnTileUsage()
    {
        return objectSpawned.GetComponent<S_Building>().tilesCoordinate;
    }

    void HidePlanePlacement()
    {
        _planePlacementValid.transform.position = new Vector3(0, -10, 0);
    }
    public void PlaceBuilding()
    {
        S_Building objectSpawnedBuildingScript = objectSpawned.GetComponent<S_Building>();
        List<Vector2Int> objectSpawnTilesUsage = GetObjectSpawnTileUsage();

        Vector2Int tmpIndexInGrid = GetObjectIndexInGridUsage(objectSpawnedBuildingScript.destination);

        if (!IsValidPlacement(tmpIndexInGrid, objectSpawnTilesUsage) || !HasEnoughMoney(objectSpawnedBuildingScript))
        {
            feelsUI.Info("Need more feels");
            return;
        }
        if (!ScriptableRounds.TryRemoveActionPoints(objectSpawnedBuildingScript.actionPointCost))
        {
            feelsUI.Info("No more action point");
            return;
        }

        UpdateGridOnPlacement(tmpIndexInGrid, objectSpawnTilesUsage, objectSpawnedBuildingScript);

        objectSpawnedBuildingScript.EndBuildingAnimation += CheckBoostBuilding;

        objectSpawnedBuildingScript.PlacedBuilding();
        _gridData.ClearPlaneFeedbackBuildingStatement();


        //Change token
        consciousTreeToken.AddAmount(1);

        if (objectSpawnedBuildingScript.GetCosts()[0].feelTypeCurrency)
            objectSpawnedBuildingScript.RemoveFeelCost();
        feelsUI.RefreshUI();


        buildingListContainer.AppendToBuildingList(objectSpawnedBuildingScript.BuildingData);

        switch (objectSpawnedBuildingScript.BuildingData.feelType)
        {
            case FeelType.Joy: joyPlaced++; break;
            case FeelType.Anger: angerPlaced++; break;
            case FeelType.Sad: sadPlaced++; break;
            case FeelType.Fear: fearPlaced++; break;
        }

        objectSpawned = null;
        HidePlanePlacement();
    }

    void ChangePlanePlacementUnderBuilding(S_Building _building)
    {
        if (_planePlacementValid)
        {
            //Plane under building
            Vector3 _planePlacementCoordinate = _building.destination + objectSpawned.transform.GetChild(0).localPosition;
            _planePlacementCoordinate.y = 0.05f;
            _planePlacementValid.transform.position = _planePlacementCoordinate;


            int x = Mathf.Abs(_building.minimumX) * _gridData.tileSize + Mathf.Abs(_building.maximumX) * _gridData.tileSize + _gridData.tileSize;
            int y = Mathf.Abs(_building.minimumY) * _gridData.tileSize + Mathf.Abs(_building.maximumY) * _gridData.tileSize + _gridData.tileSize;

            _planePlacementValid.transform.localScale = new Vector3(x, 0, y);

            //Change color on valid
            if (IsValidPlacement(GetObjectIndexInGridUsage(_building.destination), GetObjectSpawnTileUsage()))
            {
                _planePlacementValid.GetComponent<MeshRenderer>().material = _placementValid;
            }
            else _planePlacementValid.GetComponent<MeshRenderer>().material = _placementNotValid;
        }
        else
            Debug.LogWarning("Plane under building is not refer in construction manager");
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
        return _gridData.gridsUsageStatement[x][y].statement || _fogData.fogGridsUsageStatement[x][y];
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

            _gridData.SetTileUsed(x, y);
            _gridData.gridsUsageStatement[x][y].building = objectSpawned;
        }
    }



    Vector2Int GetObjectIndexInGridUsage(Vector3 objectSpawned)
    {
        //Get index base in gridUsageStatement based on position
        int indexX = (int)objectSpawned.x / _gridData.tileSize + _gridData.gridsUsageStatement.Count / 2;
        int indexZ = (int)objectSpawned.z / _gridData.tileSize + _gridData.gridsUsageStatement.Count / 2;

        return new Vector2Int(indexX, indexZ);
    }

    public void SpawnObject(GameObject gameObject)
    {
        if(objectSpawned != null)
            Destroy(objectSpawned);

        objectSpawned = SpawnGameObject(Vector3.zero, gameObject);
        _isBuilding.value = true;
    }

    GameObject SpawnGameObject(Vector3 spawnPoint, GameObject gameObject = null)
    {
        Vector3 spawnPoinTtmp = Vector3.zero;
        S_Building tmpBuilding = gameObject.GetComponent<S_Building>();

        if (objectToSpawn != null && spawnPoinTtmp != null)
        {
            GameObject tmp = Instantiate(gameObject, spawnPoinTtmp, Quaternion.identity);

            /*
            Vector3 childPos = gameObject.transform.GetChild(0).position;
            childPos.y = 0.05f;

            planePlacement = Instantiate(planePlacementValid, childPos, Quaternion.identity);
            planePlacement.transform.localScale = new Vector3(tmpBuilding.maximumX * 5, 0, tmpBuilding.maximumY * 5);

            planePlacement.transform.SetParent(gameObject.transform);
            */

            _buildingMaterial = tmp.GetComponent<Material>();
            return tmp;

        }
        else
        {
            UnityEngine.Debug.LogError("Object to spawn or spawn point not set!");
            return null;
        }

    }


    void CheckBoostBuilding(GameObject _gameObjectBuilding)
    {
        Vector2Int buildingCoordinate = GetObjectIndexInGridUsage(_gameObjectBuilding.transform.position);
        List<GameObject> _buildingsToBoost = new List<GameObject>();
        List<Vector2Int> _tilesToCheckForBoost;
        FeelType _feelType;

        //Set s_building for function
        if (_gameObjectBuilding.TryGetComponent(out S_Building s_building))
        {
            _feelType = s_building.BuildingData.feelType;
            //corner tile for sad, surrounding with other
            _tilesToCheckForBoost = (_feelType == FeelType.Sad) ? s_building.GetCornerTiles() : s_building.GetSurroundingTiles();
            if(_feelType == FeelType.Anger)
                _tilesToCheckForBoost.AddRange(s_building.GetCornerTiles());   
        }
        else
        {
            Debug.LogWarning("Missing S_Building on " + _gameObjectBuilding.name + ", check building boost abort.");
            return;
        }

        //Set s_feelAssignation
        if(_gameObjectBuilding.TryGetComponent(out S_FeelAssignationBuilding s_feelAssignation)) { }
        else
        {
            Debug.LogWarning("Missing S_FeelAssignationBuilding on " + _gameObjectBuilding.name + ", check building boost abort.");
            return;
        }

        //Get building to check for boost
        for (int i = 0; i < _tilesToCheckForBoost.Count; i++)
        {
            GameObject _currentBuildingToCheck = _gridData.GetBuildingAtTile(buildingCoordinate + _tilesToCheckForBoost[i]);

            if (_currentBuildingToCheck != null)
            {
                S_Building _currentBuildingToCheckS_Building = _currentBuildingToCheck.GetComponent<S_Building>();

                //Store all building around self
                if (_feelType == FeelType.Fear)
                {
                    _buildingsToBoost.Add(_currentBuildingToCheck);
                }
                //Else store same type
                else if (_currentBuildingToCheckS_Building.BuildingData.feelType == _feelType)
                {
                    _buildingsToBoost.Add(_currentBuildingToCheck);
                }


            }
        }

        bool tmpFearFound = false;

        //Loop to boost or unboost other building
        foreach (GameObject _building in _buildingsToBoost)
        {
            S_Building s_tmpBuilding = _building.GetComponent<S_Building>();

            //fear
            if (_feelType == FeelType.Fear && s_tmpBuilding.BuildingData.feelType == FeelType.Fear)
            {
                _building.GetComponent<S_FeelAssignationBuilding>().UnBoostBuilding();
                tmpFearFound = true;
            }
            //Anger
            else if (_feelType == FeelType.Anger && s_tmpBuilding.BuildingData.feelType == FeelType.Anger)
            {
                _building.GetComponent<S_FeelAssignationBuilding>().UnBoostBuilding();
            }
            //
            else if (_feelType == FeelType.Joy && s_tmpBuilding.BuildingData.feelType == FeelType.Joy)
            {
                _building.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
            }

            else if (_feelType == FeelType.Sad && s_tmpBuilding.BuildingData.feelType == FeelType.Sad)
            {
                _building.GetComponent<S_FeelAssignationBuilding>().BoostBuilding();
            }
        }

        //Self boost or not boost
        if (_buildingsToBoost.Count == 0 && _feelType == FeelType.Anger)
        {
            s_feelAssignation.BoostBuilding();
        }
        else if (_buildingsToBoost.Count != 0 && _feelType == FeelType.Fear && !tmpFearFound)
        {
            s_feelAssignation.BoostBuilding();
        }
        else if (_buildingsToBoost.Count != 0 && (_feelType == FeelType.Joy || _feelType == FeelType.Sad))
        {
            s_feelAssignation.BoostBuilding();
        }

    }



}
