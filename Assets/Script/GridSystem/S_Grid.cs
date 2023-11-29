using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private S_GridTileSize gridTileSize = default;
    [SerializeField] private S_GridSpheraArea gridSpheraArea = default;

    public static int tileSize;
    public static int mapSphereArea;

    public GameObject fog;

    [Tooltip("The padding is for each side.\nIt's in tile size (1 will create 1 tile padding). ")]
    public int padding_def = 1;
    public static int padding;


    public static List<List<S_GridUsage>> gridsUsageStatement = new List<List<S_GridUsage>>();

    public static List<List<bool>> fogGridsUsageStatement = new List<List<bool>>();
    public static List<List<bool>> gridDebugHighlight = new List<List<bool>>();

    public static List<List<GameObject>> fogGameObjects = new List<List<GameObject>>();

    //Set variable from editor as static
    private void Awake()
    {
        tileSize = gridTileSize.GetValue();
        mapSphereArea = gridSpheraArea.GetValue();
        padding = padding_def;
    }

    private void Start()
    {
        //Create 2 dimension table
        int tileAmountToCreate = mapSphereArea * 2 / tileSize + 1 + (padding * 2) ;
        gridsUsageStatement = Create2DimensionalList(tileAmountToCreate, () => new S_GridUsage());
        gridDebugHighlight = Create2DimensionalList(tileAmountToCreate, () => false);

        SetFogGridUsageStatement();
        CreateFogGameObjects();

        for(int i = 0; i < gridsUsageStatement.Count; ++i)
        {
            StaticBatchingUtility.Combine(fogGameObjects[i].ToArray(), null);
        }
    }

    public static void DebugHighLightTile(Vector2Int coordinate)
    {
        gridDebugHighlight[coordinate.x][coordinate.y] = true;
    }

    static public void SetTileUsed(int x, int y)
    {
        gridsUsageStatement[x][y].statement = true;
    }
    static public void RemoveTileUsed(int x, int y)
    {
        gridsUsageStatement[x][y].statement = false;
    }

    public static List<List<T>> Create2DimensionalList<T>(int size, Func<T> createInstance)
    {
        List<List<T>> dimensionalList = new List<List<T>>();

        // Create new 2D list
        for (int x = 0; x < size; x++)
        {
            List<T> tmpGrid = new List<T>();

            for (int y = 0; y < size; y++)
                tmpGrid.Add(createInstance());

            dimensionalList.Add(tmpGrid);
        }
        return dimensionalList;
    }

    void SetFogGridUsageStatement()
    {
        fogGridsUsageStatement.Clear();
        fogGridsUsageStatement = Create2DimensionalList(mapSphereArea * 2 / tileSize + 1 + (padding * 2), () => false);

        for (int i = 0; i < gridsUsageStatement.Count; i++)
        {
            for (int j = 0; j < gridsUsageStatement.Count ; j++)
            {
                if (Vector3Int.Distance(Vector3Int.zero, GetPositionBasedOnIndex(i, j)) > mapSphereArea)
                {
                    fogGridsUsageStatement[i][j] = true;
                }
            }
        }
    }

    void CreateFogGameObjects()
    {
        for(int i = 0; i < gridsUsageStatement.Count; i++)
        {
            List<GameObject> tmpList = new List<GameObject>();
            for (int j = 0;j < gridsUsageStatement.Count; j++)
            {
                if (fogGridsUsageStatement[i][j] == true)
                {
                    GameObject tmpObject = Instantiate(fog, GetPositionBasedOnIndex(i, j), Quaternion.identity);
                    tmpList.Add(tmpObject);
                }
            }
            fogGameObjects.Add(tmpList);
        }
    }

    
    static public Vector3 GetRandomTileInGrid(List<Vector2Int> sizeOfBuilding = null)
    {
        if (sizeOfBuilding.Count == 0)
            sizeOfBuilding.Add(Vector2Int.zero);


        List<Vector2Int> tmpIndex = new List<Vector2Int>();

        for(int i = 0; i < gridsUsageStatement.Count; i++)
        {
            for( int j = 0; j < gridsUsageStatement.Count; j++)
            {
                bool isEnoughSpace = true;

                //Check if enough space for building
                for(int size = 0; size < sizeOfBuilding.Count; size++)
                {
                    if (i + sizeOfBuilding[size].x < 0 || i + sizeOfBuilding[size].x > gridsUsageStatement.Count - 1
                        || j + sizeOfBuilding[size].y < 0 || j + sizeOfBuilding[size].y > gridsUsageStatement.Count - 1)
                    {
                        isEnoughSpace = false;
                        break;
                    }

                    if (gridsUsageStatement[i + sizeOfBuilding[size].x][j + sizeOfBuilding[size].y].statement 
                        || fogGridsUsageStatement[i + sizeOfBuilding[size].x][j + sizeOfBuilding[size].y])
                    {
                        isEnoughSpace = false;
                        break;
                    }
                        
                }

                if(isEnoughSpace)
                    tmpIndex.Add(new Vector2Int(i, j));
            }
        }
        int index = UnityEngine.Random.Range(0, tmpIndex.Count);

        Vector3Int tmpCoordinate;
        if (tmpIndex.Count == 0)
            tmpCoordinate = new Vector3Int(0, -1000, 0);
        else
            tmpCoordinate = GetPositionBasedOnIndex(tmpIndex[index].x, tmpIndex[index].y);
        

        return tmpCoordinate;
    }

    static public Vector3 GetRandomTileAroundOtherOne(Vector2Int BaseCoordinate, int radius, bool gridUsageStatement)
    {
        List<Vector2Int> tmpAllCoordinateAroundBase = new List<Vector2Int>();
        List<Vector2Int> tmpCoordinateFree = new List<Vector2Int>();

        for(int i = 0; i < radius * 2 + 1; i++)
        {
            for (int j = 0; j < radius * 2 + 1; j++)
            {
                tmpAllCoordinateAroundBase.Add(BaseCoordinate + new Vector2Int(i - radius, j - radius));
            }
        }

        foreach(Vector2Int coordinate in tmpAllCoordinateAroundBase)
        {
            if (coordinate.x >= 0 && coordinate.x < gridsUsageStatement.Count && coordinate.y >= 0 && coordinate.y < gridsUsageStatement.Count)
                if (gridUsageStatement)
                {
                    if (radius * tileSize >= Vector3.Distance(GetPositionBasedOnIndex(BaseCoordinate.x, BaseCoordinate.y), GetPositionBasedOnIndex(coordinate.x, coordinate.y))
                    && !gridsUsageStatement[coordinate.x][coordinate.y].statement
                    && !fogGridsUsageStatement[coordinate.x][coordinate.y])
                    {
                        tmpCoordinateFree.Add(coordinate);
                    }
                }
                else{
                    if (radius * tileSize >= Vector3.Distance(GetPositionBasedOnIndex(BaseCoordinate.x, BaseCoordinate.y), GetPositionBasedOnIndex(coordinate.x, coordinate.y))
                    && fogGridsUsageStatement[coordinate.x][coordinate.y])
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

    static private Vector3Int GetPositionBasedOnIndex(int x, int y)
    {
        int xCoord = -(gridsUsageStatement.Count / 2 * tileSize) + x * tileSize;
        int zCoord = -(gridsUsageStatement.Count / 2 * tileSize) + y * tileSize;

        return new Vector3Int(xCoord, 0, zCoord);
    }

    static public Vector2Int getIndexbasedOnPosition(Vector3 position)
    {
        int xCoord = gridsUsageStatement.Count / 2 + (int)position.x / tileSize;
        int zCoord = gridsUsageStatement.Count / 2 + (int)position.z / tileSize; ;

        return new Vector2Int(xCoord, zCoord);
    }



    static float RoundToGrid(float valueToRound, float gridSize = 1)
    {
        return Mathf.Round(valueToRound / gridSize) * gridSize;
    }

    /// <summary>
    /// This function return a Vector3, the closest one on grid based on Position input
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    static public Vector3 ClampPositionToGrid(Vector3 position)
    {
        float clampedX = RoundToGrid(position.x, Grid.tileSize);
        float clampedZ = RoundToGrid(position.z, Grid.tileSize);


        return new Vector3(clampedX, position.y, clampedZ);
    }
}
