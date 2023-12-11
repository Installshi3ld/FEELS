using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class S_BuildingData
{
    [Header("General Data")]
    public string buildingName;
    public FeelType feelType;
    public BuildingTheme BuildingTheme;

    public List<FeelTypeData> feelTypeCostList = new List<FeelTypeData>();

    [Header("Building pool")]
    public int tier = 0;
    public Sprite BuildingImage;
}

[Serializable]
public struct FeelTypeData
{
    public S_Currencies feelTypeCurrency;
    public int feelPrice;
}
#region Enumeration FeelType and Theme
public enum FeelType
{
    None = 0,
    Joy,
    Anger,
    Sad,
    Fear,
}
public enum BuildingTheme
{
    None = 0,
    Music,
    Food,
    Animals,
}
#endregion

public class S_Building : MonoBehaviour
{
    public S_BuildingData BuildingData;
    public float probabilityToSpawnInPool = 100f;

    [Space]
    public List<Vector2Int> tilesCoordinate = new List<Vector2Int>();

    [NonSerialized] public bool isPlacedAnimation, isPlaced = false;
    [NonSerialized] public Vector3 destination;
    [NonSerialized] public Vector3 location;
    float lerpAlpha = 0f;


    private void Awake()
    {
        tilesCoordinate.Add(Vector2Int.zero);
        GetMinMaxCoordinate();
    }

    int minimumX = 0, minimumY = 0, maximumX = 0, maximumY = 0;

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

    public bool HasEnoughMoney()
    {
        foreach (FeelTypeData _feelTypeData in BuildingData.feelTypeCostList)
        {
            if (!_feelTypeData.feelTypeCurrency.HasEnoughFeels(_feelTypeData.feelPrice))
                return false;
        }
        return true;
    }
    /// <summary> This function don't check if enough money, use HasEnoughMoney() to check.
    /// </summary>
    public void RemoveFeelCost()
    {
        foreach (FeelTypeData _feelTypeData in BuildingData.feelTypeCostList)
        {
            _feelTypeData.feelTypeCurrency.RemoveAmount(_feelTypeData.feelPrice);
        }
    }

    public void PlacedBuilding()
    {
        isPlacedAnimation = true;
        isPlaced = true;
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
        if (lerpAlpha > 0.5f)
            lerpAlpha = 0;
    }

    void GetMinMaxCoordinate()
    {
        for (int i = 0; i < tilesCoordinate.Count; i++)
        {
            minimumX = tilesCoordinate.Min(coord => coord.x);
            minimumY = tilesCoordinate.Min(coord => coord.y);
            maximumX = tilesCoordinate.Max(coord => coord.x);
            maximumY = tilesCoordinate.Max(coord => coord.y);
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
