using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public int gridSize_awake = 5;
    public static int gridSize;

    private void Awake()
    {
        gridSize = gridSize_awake;
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
    public static Vector3 ClampPositionToGrid(Vector3 position)
    {
        float clampedX = RoundToGrid(position.x, gridSize);
        float clampedZ = RoundToGrid(position.z, gridSize);


        return new Vector3(clampedX, position.y, clampedZ);
    }


}
