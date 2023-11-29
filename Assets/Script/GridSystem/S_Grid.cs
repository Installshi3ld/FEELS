using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private S_GridData gridData = default;

    public GameObject fog;

    [Tooltip("The padding is for each side.\nIt's in tile size (1 will create 1 tile padding). ")]

    public static List<List<bool>> fogGridsUsageStatement = new List<List<bool>>();
    public static List<List<bool>> gridDebugHighlight = new List<List<bool>>();

    public static List<List<GameObject>> fogGameObjects = new List<List<GameObject>>();

    private int _tileSize;
    private int _mapSphereArea;
    private int _padding;
    int _tileAmount;

    private List<List<S_GridUsage>> _gridUsage;

    //Set variable from editor as static
    private void Awake()
    {
        _tileSize = gridData.tileSize;
        _mapSphereArea = gridData.mapSphereArea;
        _padding = gridData.padding;

        _tileAmount = _mapSphereArea * 2 / _tileSize + 1 + (_padding * 2);

        gridData.InitializeGridUsage(_tileAmount);
        _gridUsage = gridData.gridsUsageStatement;
    }

    private void Start()
    {
        //Create 2 dimension table
        gridDebugHighlight = S_StaticFunc.Create2DimensionalList(_tileAmount, () => false);

        SetFogGridUsageStatement();
        CreateFogGameObjects();

        for(int i = 0; i < _tileAmount; ++i)
        {
            StaticBatchingUtility.Combine(fogGameObjects[i].ToArray(), null);
        }
    }

    public static void DebugHighLightTile(Vector2Int coordinate)
    {
        gridDebugHighlight[coordinate.x][coordinate.y] = true;
    }

    void SetFogGridUsageStatement()
    {
        fogGridsUsageStatement.Clear();
        fogGridsUsageStatement = S_StaticFunc.Create2DimensionalList(_mapSphereArea * 2 / _tileSize + 1 + (_padding * 2), () => false);

        for (int i = 0; i < _tileAmount; i++)
        {
            for (int j = 0; j < _tileAmount; j++)
            {
                if (Vector3Int.Distance(Vector3Int.zero, gridData.GetPositionBasedOnIndex(i, j)) > _mapSphereArea)
                {
                    fogGridsUsageStatement[i][j] = true;
                }
            }
        }
    }

    void CreateFogGameObjects()
    {
        for(int i = 0; i < _tileAmount; i++)
        {
            List<GameObject> tmpList = new List<GameObject>();
            for (int j = 0;j < _tileAmount; j++)
            {
                if (fogGridsUsageStatement[i][j] == true)
                {
                    GameObject tmpObject = Instantiate(fog, gridData.GetPositionBasedOnIndex(i, j), Quaternion.identity);
                    tmpList.Add(tmpObject);
                }
            }
            fogGameObjects.Add(tmpList);
        }
    }

}
