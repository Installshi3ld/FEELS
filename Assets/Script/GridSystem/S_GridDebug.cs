using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class S_GridDebug : MonoBehaviour
{
    [SerializeField] private S_GridDebugTileInt _debugTile;
    [SerializeField] private S_GridData _gridData;

    List<List<bool>> gridDebugHighlight = new List<List<bool>>();

    private int _debugTileInt, _mapTileSize;

    //Debug.LogWarning($"Missing reference in S_GridDebug : DebugTileInt = {_debugTileInt} | GridData = {((_gridData == null) ? "NULL" : _gridData)}{Environment.StackTrace}");
    //Debug.LogWarning("Missing reference in S_GridDebug : DebugTileInt = " + _debugTileInt + "| GridData = " + _gridData);
    //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    //Application.persistentDataPath
    private void Start()
    {
        _gridData.Init();
        gridDebugHighlight = S_StaticFunc.Create2DimensionalList(_gridData.tileAmount, () => false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(_debugTile == null || _gridData == null)
            {
                throw new NotImplementedException($"Missing reference in S_GridDebug : DebugTileInt = {_debugTileInt} | GridData = {((_gridData == null) ? "NULL" : _gridData)}");
                //return;
            }
            _debugTile.IncrementValue();
        }
    }

    public void DebugHighLightTile(Vector2Int coordinate)
    {
        gridDebugHighlight[coordinate.x][coordinate.y] = true;
    }

    private void OnDrawGizmos()
    {
        if(_gridData == null  || _gridData.gridsUsageStatement == null)
            return;

        if(_debugTile == null)
            return;

        DrawGizmoDisk(_gridData.mapSphereArea);
        _debugTileInt = _debugTile.GetValue();
        _mapTileSize = _gridData.tileSize;
        if (_debugTileInt > 0)
        {
            for (int x = 0; x < _gridData.gridsUsageStatement.Count; x++)
            {
                for (int y = 0; y < _gridData.gridsUsageStatement[x].Count; y++)
                {
                    if (_gridData.gridsUsageStatement[x][y].statement && (_debugTileInt == 1 || _debugTileInt == 3))
                        Gizmos.color = Color.green;

                    else if (Grid.fogGridsUsageStatement[x][y] && (_debugTileInt == 2 || _debugTileInt == 3))
                        Gizmos.color = Color.blue;

                    else if (!Grid.fogGridsUsageStatement[x][y] && (_debugTileInt == 1 || _debugTileInt == 3))
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.clear;

                    if (gridDebugHighlight[x][y])
                        Gizmos.color = new Vector4(255, 255 / 198, 255 / 41, 1);

                    Gizmos.DrawWireCube(new Vector3(_gridData.gridsUsageStatement[x].Count / 2 * -_mapTileSize + x * _mapTileSize,
                        0,
                        _gridData.gridsUsageStatement[y].Count / 2 * -_mapTileSize + y * _mapTileSize), new Vector3(_mapTileSize - 0.6f, _mapTileSize - 0.6f, _mapTileSize - 0.6f));

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
}


