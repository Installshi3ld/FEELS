using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static S_Building;

public class Grid : MonoBehaviour
{
    
    public int tileSize_def = 5;
    public static int tileSize;

    public int mapSphereArea_def = 60;
    public static int mapSphereArea;

    public GameObject fog;

    [Tooltip("The padding is for each side.\nIt's in tile size (1 will create 1 tile padding). ")]
    public int padding_def = 1;
    public static int padding;

    int debugTileStatement = 0;
    bool debugTiles = false;
    bool debugFog = false;

    public static List<List<bool>> gridsUsageStatement = new List<List<bool>>();

    public static List<List<bool>> fogGridsUsageStatement = new List<List<bool>>();

    //Set variable from editor as static
    private void Awake()
    {
        tileSize = tileSize_def;
        mapSphereArea = mapSphereArea_def;
        padding = padding_def;
    }

    private void Start()
    {
        //Create 2 dimension table
        int tileAmountToCreate = mapSphereArea * 2 / tileSize + 1 + (padding * 2) ;
        gridsUsageStatement = Create2DimensionalBoolList(tileAmountToCreate);
        
        SetFogGridUsageStatement();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            debugTileStatement++;
            if (debugTileStatement >= 4)
                debugTileStatement = 0;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            fog.transform.localScale = new Vector3 (fog.transform.localScale.x + 20, 45, fog.transform.localScale.z + 20);
            IncreaseMapSphereArea(1);
        }

    }

    private void OnDrawGizmos()
    {
        DrawGizmoDisk(mapSphereArea);

        if (debugTileStatement > 0)
        {
            for (int x = 0; x < gridsUsageStatement.Count; x++)
            {
                for (int y = 0; y < gridsUsageStatement[x].Count; y++)
                {
                    if (gridsUsageStatement[x][y] && (debugTileStatement == 1 || debugTileStatement == 3))
                        Gizmos.color = Color.green;

                    else if (fogGridsUsageStatement[x][y] && (debugTileStatement == 2 || debugTileStatement == 3)) 
                        Gizmos.color = Color.blue;

                    else if (!fogGridsUsageStatement[x][y] && (debugTileStatement == 1 || debugTileStatement == 3))
                        Gizmos.color = Color.red;

                    else
                        Gizmos.color = Color.clear;

                    Gizmos.DrawWireCube(new Vector3(gridsUsageStatement[x].Count / 2 * -tileSize + x * tileSize,
                        0,
                        gridsUsageStatement[y].Count / 2 * -tileSize + y * tileSize), new Vector3(tileSize - 0.6f, tileSize - 0.6f, tileSize - 0.6f));

                }
            }
        }
    }

    public static void DrawGizmoDisk(float radius)
    {
        float corners = 72; 
        Vector3 origin = Vector3.zero; 
        Vector3 startRotation = Vector3.right * radius; 
        Vector3 lastPosition = origin + startRotation;
        float angle = 0;
        while (angle <= 360)
        {
            angle += 360 / corners;
            Vector3 nextPosition = origin + (Quaternion.Euler(90, 0, angle) * startRotation);
            Gizmos.DrawLine(lastPosition, nextPosition);
            lastPosition = nextPosition;
        }
    }



    /// <summary>
    /// The number of tile to add
    /// </summary>
    /// <param name="tilesAmount">The number of tile to add</param>
    void IncreaseMapSphereArea(int tilesAmount)
    {
        mapSphereArea += tilesAmount * tileSize;

        int tileAmountToCreate = mapSphereArea * 2 / tileSize + 1 + (padding * 2);
        List<List<bool>> newGridsUsageStatement = Create2DimensionalBoolList(tileAmountToCreate);


        //Set old data in new list
        for (int i = 0; i < newGridsUsageStatement.Count - tilesAmount * 2; i++)
        {
            for (int j = 0; j < newGridsUsageStatement[i].Count - tilesAmount * 2; j++)
            {

                if (gridsUsageStatement[i][j])
                {
                    newGridsUsageStatement[i + tilesAmount][j + tilesAmount] = true;
                }
            }
        }

        gridsUsageStatement = newGridsUsageStatement;
        SetFogGridUsageStatement();
    }

    List<List<bool>> Create2DimensionalBoolList(int size)
    {
        List<List<bool>> dimensionalList = new List<List<bool>>();

        //Create new 2 dimension list
        for (int x = 0; x < size; x++)
        {
            List<bool> tmpGrid = new List<bool>();
            
            for (int y = 0; y < size; y++)
                tmpGrid.Add(false);

            dimensionalList.Add(tmpGrid);
        }

        return dimensionalList;
    }

    void SetFogGridUsageStatement()
    {
        fogGridsUsageStatement.Clear();
        fogGridsUsageStatement = Create2DimensionalBoolList(mapSphereArea * 2 / tileSize + 1 + (padding * 2));

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

    
    Vector2Int GetRandomTileInGrid()
    {
        List<Vector2Int> tmpIndex = new List<Vector2Int>();

        for(int i = 0; i < gridsUsageStatement.Count; i++)
        {
            for( int j = 0; j < gridsUsageStatement.Count; j++)
            {
                if (!gridsUsageStatement[i][j] && !fogGridsUsageStatement[i][j])
                    tmpIndex.Add(new Vector2Int(i, j));
            }
        }
        int index = Random.Range(0, tmpIndex.Count);

        return tmpIndex[index];
    }

    Vector3Int GetPositionBasedOnIndex(int x, int y)
    {
        int xCoord = -(gridsUsageStatement.Count / 2 * tileSize) + x * tileSize;
        int zCoord = -(gridsUsageStatement.Count / 2 * tileSize) + y * tileSize;

        return new Vector3Int(xCoord, 0, zCoord);
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
