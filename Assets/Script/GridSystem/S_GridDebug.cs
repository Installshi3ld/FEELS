using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GridDebug : MonoBehaviour
{
    public S_GridDebugTileInt debugTileInt = default(S_GridDebugTileInt);
    public S_GridSpheraArea SphereArea = default(S_GridSpheraArea);
    public S_GridTileSize TileSize = default(S_GridTileSize);

    private int _debugTileInt, _mapTileSize;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            debugTileInt.IncrementValue();
        }
    }
    private void OnDrawGizmos()
    {
        DrawGizmoDisk(SphereArea.GetValue());

        _debugTileInt = debugTileInt.GetValue();
        _mapTileSize = TileSize.GetValue();
        if (_debugTileInt > 0)
        {
            for (int x = 0; x < Grid.gridsUsageStatement.Count; x++)
            {
                for (int y = 0; y < Grid.gridsUsageStatement[x].Count; y++)
                {
                    if (Grid.gridsUsageStatement[x][y].statement && (_debugTileInt == 1 || _debugTileInt == 3))
                        Gizmos.color = Color.green;

                    else if (Grid.fogGridsUsageStatement[x][y] && (_debugTileInt == 2 || _debugTileInt == 3))
                        Gizmos.color = Color.blue;

                    else if (!Grid.fogGridsUsageStatement[x][y] && (_debugTileInt == 1 || _debugTileInt == 3))
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.clear;

                    if (Grid.gridDebugHighlight[x][y])
                        Gizmos.color = new Vector4(255, 255 / 198, 255 / 41, 1);

                    Gizmos.DrawWireCube(new Vector3(Grid.gridsUsageStatement[x].Count / 2 * -_mapTileSize + x * _mapTileSize,
                        0,
                        Grid.gridsUsageStatement[y].Count / 2 * -_mapTileSize + y * _mapTileSize), new Vector3(_mapTileSize - 0.6f, _mapTileSize - 0.6f, _mapTileSize - 0.6f));

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


