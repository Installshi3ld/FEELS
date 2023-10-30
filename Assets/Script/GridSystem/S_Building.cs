using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Building : MonoBehaviour
{
    [Tooltip("Consider X Y as X Z \n The root is always 0, 0 (Be sure to add it) \n Then add tile next to it, each one \n For example a T form will be :\n - X:0 Y:0\n - X:-1 Y:0\n - X:1 Y:0\n - X:0 Y:-1")]
    public List<Vector2Int> tilesCoordinate = new List<Vector2Int>();

    private void Awake()
    {
        tilesCoordinate.Add(Vector2Int.zero);
    }
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

    float lerpAlpha = 0f;
    public bool isPlaced = false;
    public Vector3 destination;
    private void Update()
    {
        if (!isPlaced)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, destination, lerpAlpha);

            lerpAlpha += 1f * Time.deltaTime * 3;
            /*
            if (Vector3.Distance(this.transform.position, destination) < 0.01f)
            {
                this.transform.position = destination;
            }*/
        }
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
        if( lerpAlpha > 0.5f )
            lerpAlpha = 0;
    }

    /*
    public IEnumerator SmoothObjectPositionBetweenVector(Vector3 destination)
    {
        float lerpAlpha = 0f;
        while (lerpAlpha < 1 && this != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, destination, lerpAlpha);
            
            lerpAlpha += 1f * Time.deltaTime;
            if(Vector3.Distance(this.transform.position, destination) < 3)
            {
                this.transform.position = destination;
                break;
            }
            yield return new WaitForEndOfFrame();
        }

    }*/

    public List<FeelsCost> feelsCostList = new List<FeelsCost>();   


    /// <summary>
    /// This function return a Vector3, the closest one on grid based on Position input
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    

}
