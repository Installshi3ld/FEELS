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

    [SerializeField]private S_FogData _fogData;

    public void Init()
    {
        tileAmount = mapSphereArea * 2 / tileSize + 1 + (padding * 2);
        gridsUsageStatement = S_StaticFunc.Create2DimensionalList(tileAmount, () => new S_GridUsage());
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

    public void SetTileUsed(int x, int y)
    {
        gridsUsageStatement[x][y].statement = true;
    }
    public void RemoveTileUsed(int x, int y)
    {
        gridsUsageStatement[x][y].statement = false;
    }

    private float RoundToGrid(float valueToRound, float gridSize = 1)
    {
        return Mathf.Round(valueToRound / gridSize) * gridSize;
    }

    public Vector3 ClampPositionToGrid(Vector3 position)
    {
        float _clampedX = RoundToGrid(position.x, tileSize);
        float _clampedZ = RoundToGrid(position.z, tileSize);

        return new Vector3(_clampedX, position.y, _clampedZ);
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
