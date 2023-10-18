using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Building : MonoBehaviour
{
    [Tooltip("Consider X Y as X Z \n The root is always 0, 0 (Be sure to add it) \n Then add tile next to it, each one \n For example a T form will be :\n - X:0 Y:0\n - X:-1 Y:0\n - X:1 Y:0\n - X:0 Y:-1")]
    public List<Vector2Int> tilesCoordinate = new List<Vector2Int>();

    public enum FeelsType
    {
        Joy,
        Anger,
        Sad,
        Fear
    };


    [System.Serializable]
    public struct FeelsCost
    {
        public FeelsType feelsType;
        public int cost;
    }

    public List<FeelsCost> feelsCostList = new List<FeelsCost>();   


    /// <summary>
    /// This function return a Vector3, the closest one on grid based on Position input
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    

}
