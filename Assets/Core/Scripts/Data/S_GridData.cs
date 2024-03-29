using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_GridData", menuName = "Data/SO_Grid Data")]
public class S_GridData : ScriptableObject, InitializeSO
{
    public int tileSize;
    public int mapSphereArea;
    public int padding;

    [NonSerialized] public int tileAmount;
    [SerializeField] public List<List<S_GridUsage>> gridsUsageStatement;

    //Set by S_GridDebug
    [NonSerialized] public List<List<GameObject>> __planeFeedbackBoostBuildingList = new List<List<GameObject>>();

    [SerializeField]public S_FogData _fogData;

    public delegate void RefreshDebugTileDelegate(int x, int y);
    public RefreshDebugTileDelegate RefreshDebugTile;

    public void Init()
    {
        tileAmount = mapSphereArea * 2 / tileSize + 1 + (padding * 2);
        gridsUsageStatement = S_StaticFunc.Create2DimensionalList(tileAmount, () => new S_GridUsage());
        _fogData.Init(tileAmount);
    }

    public Vector2Int GetIndexbasedOnPosition(Vector3 position)
    {
        int xCoord = gridsUsageStatement.Count / 2 + (int)position.x / tileSize;
        int zCoord = gridsUsageStatement.Count / 2 + (int)position.z / tileSize; ;

        return new Vector2Int(xCoord, zCoord);
    }

    public Vector3Int GetPositionBasedOnIndex(int x, int y)
    {
        int xCoord = -(gridsUsageStatement.Count / 2 * tileSize) + x * tileSize;
        int zCoord = -(gridsUsageStatement.Count / 2 * tileSize) + y * tileSize;

        return new Vector3Int(xCoord, 0, zCoord);
    }

    //Get building script
    public GameObject GetBuildingAtTile(Vector2Int coord)
    {
        return gridsUsageStatement[coord.x][coord.y].building;
    }

    public void SetTileUsed(int x, int y)
    {
        gridsUsageStatement[x][y].statement = true;
        RefreshDebugTile(x, y);
    }
    public void RemoveTileUsed(int x, int y)
    {
        gridsUsageStatement[x][y].statement = false;
    }

    private float RoundToGrid(float valueToRound, float gridSize = 1)
    {
        return Mathf.Round(valueToRound / gridSize) * gridSize;
    }

    //To know if coord is in Array lenght
    public bool IsTileCoordinateInBound(Vector2Int Coord)
    {
        return 0 <= Coord.x && Coord.x <= mapSphereArea * 2 / tileSize + padding * 2
                && 0 <= Coord.y && Coord.y <= mapSphereArea * 2 / tileSize + padding * 2;
    }

    public Vector3 ClampPositionToGrid(Vector3 position)
    {
        float _clampedX = RoundToGrid(position.x, tileSize);
        float _clampedZ = RoundToGrid(position.z, tileSize);

        return new Vector3(_clampedX, position.y, _clampedZ);
    }

    public void SetPlaneFeedbackBuildingStatement(int x, int y, bool statement)
    {
        __planeFeedbackBoostBuildingList[x][y].SetActive(statement);
    }

    public void SetAllPlaneFeedbackBuildingEnable()
    {
        for (int i = 0; i < __planeFeedbackBoostBuildingList.Count; i++)
        {
            for (int j = 0; j < __planeFeedbackBoostBuildingList[i].Count; j++)
            {
                __planeFeedbackBoostBuildingList[i][j].SetActive(true);
            }
        }
    }

    public void ClearPlaneFeedbackBuildingStatement()
    {
        for (int i = 0; i < __planeFeedbackBoostBuildingList.Count; i++)
        {
            for (int j = 0; j < __planeFeedbackBoostBuildingList[i].Count; j++)
            {
                __planeFeedbackBoostBuildingList[i][j].SetActive(false);
            }
        }
    }

    public bool IsTileEmpty(Vector2Int coord)
    {
        return !gridsUsageStatement[coord.x][coord.y].statement && !_fogData.fogGridsUsageStatement[coord.x][coord.y];
    }

    public Vector3 GetRandomTileAroundOtherOne(Vector2Int BaseCoordinate, int radius, bool gridUsageStatement)
    {
        List<Vector2Int> tmpAllCoordinateAroundBase = new List<Vector2Int>();
        List<Vector2Int> tmpCoordinateFree = new List<Vector2Int>();

        for (int i = 0; i < radius * 2 + 1; i++)
        {
            for (int j = 0; j < radius * 2 + 1; j++)
            {
                tmpAllCoordinateAroundBase.Add(BaseCoordinate + new Vector2Int(i - radius, j - radius));
            }
        }

        foreach (Vector2Int coordinate in tmpAllCoordinateAroundBase)
        {
            if (coordinate.x >= 0 && coordinate.x < gridsUsageStatement.Count && coordinate.y >= 0 && coordinate.y < gridsUsageStatement.Count)
                if (gridUsageStatement)
                {
                    if (radius * tileSize >= Vector3.Distance(GetPositionBasedOnIndex(BaseCoordinate.x, BaseCoordinate.y), GetPositionBasedOnIndex(coordinate.x, coordinate.y))
                    && !gridsUsageStatement[coordinate.x][coordinate.y].statement
                    && !_fogData.fogGridsUsageStatement[coordinate.x][coordinate.y])
                    {
                        tmpCoordinateFree.Add(coordinate);
                    }
                }
                else
                {
                    if (radius * tileSize >= Vector3.Distance(GetPositionBasedOnIndex(BaseCoordinate.x, BaseCoordinate.y), GetPositionBasedOnIndex(coordinate.x, coordinate.y))
                    && _fogData.fogGridsUsageStatement[coordinate.x][coordinate.y])
                    {
                        tmpCoordinateFree.Add(coordinate);
                    }
                }
        }

        if (tmpCoordinateFree.Count <= 0)
            return Vector3.zero;

        int tmpRandomIndex = UnityEngine.Random.Range(0, tmpCoordinateFree.Count - 1);

        Vector3 tmpCoordinate = GetPositionBasedOnIndex(tmpCoordinateFree[tmpRandomIndex].x, tmpCoordinateFree[tmpRandomIndex].y);
        return tmpCoordinate;
    }


    public Vector3 GetRandomTileInGrid(List<Vector2Int> tilesUsed = null)
    {
        if (tilesUsed.Count == 0)  tilesUsed.Add(Vector2Int.zero);


        int gridUsageCount = gridsUsageStatement.Count;
        List<Vector2Int> tmpIndex = new List<Vector2Int>();

        for (int i = 0; i < gridUsageCount; i++)
        {
            for (int j = 0; j < gridUsageCount; j++)
            {
                bool isEnoughSpace = true;

                //Check if enough space for building
                for (int sizeIndex = 0; sizeIndex < tilesUsed.Count; sizeIndex++)
                {
                    Vector2Int tileUsedCoord = tilesUsed[sizeIndex];
                    if (i + tileUsedCoord.x < 0 || i + tileUsedCoord.x > gridUsageCount - 1
                        || j + tileUsedCoord.y < 0 || j + tileUsedCoord.y > gridUsageCount - 1)
                    {
                        isEnoughSpace = false;
                        break;
                    }

                    if (gridsUsageStatement[i + tileUsedCoord.x][j + tileUsedCoord.y].statement
                        || _fogData.fogGridsUsageStatement[i + tileUsedCoord.x][j + tileUsedCoord.y])
                    {
                        isEnoughSpace = false;
                        break;
                    }

                }

                if (isEnoughSpace)
                    tmpIndex.Add(new Vector2Int(i, j));
            }
        }
        int index = UnityEngine.Random.Range(0, tmpIndex.Count);

        //Return insane Y if no tile available
        return tmpIndex.Count == 0 ? new Vector3Int(0, -1000, 0) : GetPositionBasedOnIndex(tmpIndex[index].x, tmpIndex[index].y);
    }

}
