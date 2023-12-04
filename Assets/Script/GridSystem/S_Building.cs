using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_Building : MonoBehaviour
{
    #region variable
    [Tooltip("Consider X Y as X Z \n The root is always 0, 0 (Be sure to add it) \n Then add tile next to it, each one \n For example a T form will be :\n - X:0 Y:0\n - X:-1 Y:0\n - X:1 Y:0\n - X:0 Y:-1")]
    public List<Vector2Int> tilesCoordinate = new List<Vector2Int>();

    public int tier = 0;
    public float probabilityToSpawnInPool = 100f;
    public Sprite BuildingImage;

    float lerpAlpha = 0f;
    [System.NonSerialized]
    public bool isPlacedAnimation, isPlaced = false;

    [System.NonSerialized]
    public Vector3 destination;

    public S_Currencies FeelCurrency;
    public int price = 0;

    public delegate void ChangingEquilibriumValue();
    public event ChangingEquilibriumValue changingEquilibriumValue;

    #endregion

    private void Awake()
    {
        tilesCoordinate.Add(Vector2Int.zero);
    }
    int minimumX = 0, minimumY = 0, maximumX = 0, maximumY = 0;
    private void Start()
    {
        GetMinMaxCoordinate();
    }
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
        if (FeelCurrency)
            changingEquilibriumValue();
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
        if( lerpAlpha > 0.5f )
            lerpAlpha = 0;
    }
    
    void GetMinMaxCoordinate()
    {
        for (int i = 0; i < tilesCoordinate.Count; i++)
        {
            if (tilesCoordinate[i].x < minimumX)
                minimumX = tilesCoordinate[i].x;
            if (tilesCoordinate[i].y < minimumY)
                minimumY = tilesCoordinate[i].y;
            if (tilesCoordinate[i].x > maximumX)
                maximumX = tilesCoordinate[i].x;
            if (tilesCoordinate[i].y > maximumY)
                maximumY = tilesCoordinate[i].y;

        }
    }

    public List<Vector2Int> GetCornerTiles()
    {
        List<Vector2Int> surroundingTiles = new List<Vector2Int>();
        for (int i = 0; i < tilesCoordinate.Count; i++)
        {
            if (tilesCoordinate[i].x == minimumX && tilesCoordinate[i].y == minimumY)
                surroundingTiles.Add(new Vector2Int(tilesCoordinate[i].x - 1, tilesCoordinate[i].y - 1));

            if (tilesCoordinate[i].x == minimumX && tilesCoordinate[i].y == maximumY)
                surroundingTiles.Add(new Vector2Int(tilesCoordinate[i].x - 1, tilesCoordinate[i].y + 1));

            if (tilesCoordinate[i].x == maximumX && tilesCoordinate[i].y == minimumY)
                surroundingTiles.Add(new Vector2Int(tilesCoordinate[i].x + 1, tilesCoordinate[i].y - 1));

            if (tilesCoordinate[i].x == maximumY && tilesCoordinate[i].y == maximumY)
                surroundingTiles.Add(new Vector2Int(tilesCoordinate[i].x + 1, tilesCoordinate[i].y + 1));
        }
        return surroundingTiles;
    }
    public List<Vector2Int> GetSurroundingTiles()
    {
        List<Vector2Int> surroundingTiles = new List<Vector2Int>();
        for (int i = 0; i < tilesCoordinate.Count; i++)
        {
            if (tilesCoordinate[i].x == minimumX)
                surroundingTiles.Add(new Vector2Int(tilesCoordinate[i].x - 1, tilesCoordinate[i].y));

            if (tilesCoordinate[i].y == minimumY)
                surroundingTiles.Add(new Vector2Int(tilesCoordinate[i].x, tilesCoordinate[i].y - 1));

            if (tilesCoordinate[i].x == maximumX)
                surroundingTiles.Add(new Vector2Int(tilesCoordinate[i].x + 1, tilesCoordinate[i].y));

            if (tilesCoordinate[i].y == maximumY)
                surroundingTiles.Add(new Vector2Int(tilesCoordinate[i].x, tilesCoordinate[i].y + 1));
        }

        return surroundingTiles;
    }
}
