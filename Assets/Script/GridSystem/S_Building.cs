using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Building : MonoBehaviour
{
    [Tooltip("Consider X Y as X Z \n The root is always 0, 0 (Be sure to add it) \n Then add tile next to it, each one \n For example a T form will be :\n - X:0 Y:0\n - X:-1 Y:0\n - X:1 Y:0\n - X:0 Y:-1")]
    public List<Vector2Int> tilesCoordinate = new List<Vector2Int>();

    float RoundToGrid(float valueToRound, float gridSize = 1)
    {
        return Mathf.Round(valueToRound / gridSize) * gridSize;
    }

    /// <summary>
    /// This function return a Vector3, the closest one on grid based on Position input
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector3 ClampPositionToGrid(Vector3 position)
    {
        float clampedX = RoundToGrid(position.x, Grid.tileSize);
        float clampedZ = RoundToGrid(position.z, Grid.tileSize);


        return new Vector3(clampedX, position.y, clampedZ);
    }

}
