using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

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

[CreateAssetMenu(fileName = "SO_BuildingData", menuName = "Data/BuildingData")]
public class S_BuildingData : ScriptableObject
{
    [Header("General Data")]
    public string buildingName;
    public FeelType feelType;
    public BuildingTheme BuildingTheme;

    public List<FeelTypeData> feelTypeCostList = new List<FeelTypeData>();

    [Header("Building pool")]
    public int tier = 0;
    public float probabilityToSpawnInPool = 100f;
    public Sprite BuildingImage;

    [Space]
    public List<Vector2Int> tilesCoordinate = new List<Vector2Int>();

    [NonSerialized] public bool isPlacedAnimation, isPlaced = false;
    [NonSerialized] public Vector3 destination;
    [NonSerialized] public Vector3 location;

    private void Awake()
    {
        tilesCoordinate.Add(Vector2Int.zero);
        GetMinMaxCoordinate();
    }

    int minimumX = 0, minimumY = 0, maximumX = 0, maximumY = 0;


    public bool HasEnoughMoney()
    {
        foreach(FeelTypeData _feelTypeData in feelTypeCostList)
        {
            if(!_feelTypeData.feelTypeCurrency.HasEnoughFeels(_feelTypeData.feelPrice))
                return false;
        }
        return true;
    }
    /// <summary>
    /// This function don't check if enough money, use HasEnoughMoney() to check.
    /// </summary>
    public void RemoveFeelCost()
    {
        foreach (FeelTypeData _feelTypeData in feelTypeCostList)
        {
            _feelTypeData.feelTypeCurrency.RemoveAmount(_feelTypeData.feelPrice);
        }
    }

    public void PlacedBuilding()
    {
        isPlaced = true;
        isPlacedAnimation = true;
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
        /*
        if (lerpAlpha > 0.5f)
            lerpAlpha = 0;*/
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
