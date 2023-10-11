using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    
    public int tileSize_def = 5;
    public static int tileSize;

    public int roundRange_def = 60;
    public static int mapSphereArea;

    [Tooltip("The padding is for each side.\nIt's in tile size (1 will create 1 tile padding).")]
    public int padding_def = 1;
    public static int padding;

    bool debugTiles = false;

    public static List<List<bool>> gridsUsageStatement = new List<List<bool>>();

    //Set variable from editor as static
    private void Awake()
    {
        tileSize = tileSize_def;
        mapSphereArea = roundRange_def;
        padding = padding_def * tileSize;
    }

    private void Start()
    {
        //Create 2 dimension table
        int tileAmountToCreate = mapSphereArea * 2 / tileSize + 1;
        gridsUsageStatement = Create2DimensionalBoolList(tileAmountToCreate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            debugTiles = !debugTiles;

        if (Input.GetKeyDown(KeyCode.F2))
            IncreaseMapSphereArea(1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, mapSphereArea - padding); // - 5 Cause need one tile each side for Fog

        if (debugTiles)
        {
            for (int x = 0; x < gridsUsageStatement.Count; x++)
            {
                for (int y = 0; y < gridsUsageStatement[x].Count; y++)
                {
                    if (gridsUsageStatement[x][y])
                        Gizmos.color = Color.green;
                    else
                        Gizmos.color = Color.red;

                    Gizmos.DrawWireCube(new Vector3(gridsUsageStatement[x].Count / 2 * -tileSize + x * tileSize,
                        0,
                        gridsUsageStatement[y].Count / 2 * -tileSize + y * tileSize), new Vector3(tileSize - 0.6f, tileSize - 0.6f, tileSize - 0.6f));

                }
            }
        }
    }



    /// <summary>
    /// The number of tile to add
    /// </summary>
    /// <param name="tilesAmount">The number of tile to add</param>
    void IncreaseMapSphereArea(int tilesAmount)
    {
        mapSphereArea += tilesAmount * tileSize;

        int tileAmountToCreate = mapSphereArea * 2 / tileSize + 1;
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


}
