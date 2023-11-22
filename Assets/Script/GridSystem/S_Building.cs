using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Building : MonoBehaviour
{
    [Tooltip("Consider X Y as X Z \n The root is always 0, 0 (Be sure to add it) \n Then add tile next to it, each one \n For example a T form will be :\n - X:0 Y:0\n - X:-1 Y:0\n - X:1 Y:0\n - X:0 Y:-1")]
    public List<Vector2Int> tilesCoordinate = new List<Vector2Int>();

    public int tier = 0;
    public float probabilityToSpawnInPool = 100f;
    public Sprite BuildingImage;
    private void Awake()
    {
        tilesCoordinate.Add(Vector2Int.zero);
    }

    float lerpAlpha = 0f;
    [System.NonSerialized]
    public bool isPlacedAnimation, isPlaced = false;

    [System.NonSerialized]
    public Vector3 destination;

    public S_Currencies FeelType;
    public int price = 0;

    public delegate void ChangingEquilibriumValue();
    public event ChangingEquilibriumValue changingEquilibriumValue;

    private void Update()
    {
        if (!isPlacedAnimation)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, destination, lerpAlpha);

            lerpAlpha += 1f * Time.deltaTime * 3;
            
            if (isPlaced && Vector3.Distance(this.transform.position, destination) < 0.05f)
            {
                this.transform.position = destination;
                isPlacedAnimation = true;
            }
        }
    }

    public void PlacedBuilding()
    {
        isPlaced = true;
        isPlacedAnimation = true;
        if (FeelType)
            changingEquilibriumValue();
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
        if( lerpAlpha > 0.5f )
            lerpAlpha = 0;
    }

}
