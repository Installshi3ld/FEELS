using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class S_BuildingData : IEquatable<S_BuildingData> 
{
    [Header("General Data")]
    public string buildingName;
    public FeelType feelType;
    public BuildingTheme BuildingTheme;

    public List<FeelTypeData> feelTypeCostList = new List<FeelTypeData>();

    [Header("Building pool")]
    public int tier = 0;
    public Sprite BuildingImage;

    public bool Equals(S_BuildingData other)
    {
        return other != null &&
            other.buildingName == this.buildingName &&
            other.feelType == this.feelType &&
            other.tier == this.tier &&
            other.BuildingTheme == this.BuildingTheme;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(buildingName, feelType, tier, BuildingTheme);
    }

    [NonSerialized]public S_Building building;
}

[Serializable]
public struct FeelTypeData 
{
    public S_Currencies feelTypeCurrency;
    public int feelPrice;

    public bool HasEnoughFeels()
    {
        return feelTypeCurrency.HasEnoughFeels(feelPrice);
    }

    public void Pay()
    { 
        feelTypeCurrency.RemoveAmount(feelPrice);
    }
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
    public S_VFXData VFXData;

    [Space]
    public List<Vector2Int> tilesCoordinate = new List<Vector2Int>();

    [NonSerialized] public bool isPlacedAnimation, isPlaced = false;
    [NonSerialized] public Vector3 destination;

    float lerpAlpha = 0f;

    [SerializeField] private S_BuildingCostManager costManager;

    private void Awake()
    {
        tilesCoordinate.Add(Vector2Int.zero);
        GetMinMaxCoordinate();
        BuildingData.building = this;
    }

    public int minimumX = 0, minimumY = 0, maximumX = 0, maximumY = 0;

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
                GetOutOfGroundAnimation();

                //Dust VFX
                buildingVFX = Instantiate(VFXData.DustVFX.effects[0], this.transform);
                buildingVFX.transform.position = GetRootCoordinate();
            }
        }
    }
    GameObject buildingVFX;
    public void GetOutOfGroundAnimation()
    {
        Transform tmpChild = this.transform.GetChild(0).transform;
        tmpChild.position = new Vector3(tmpChild.position.x, -10, tmpChild.position.z);
        tmpChild.transform.DOMoveY(0, 2)
            .OnComplete(() => {
                //Explosion end building
                GameObject tmpFX = Instantiate(VFXData.GetVFXEndOfConstruction(), this.transform);
                tmpFX.transform.position = GetRootCoordinate();

                Destroy(buildingVFX);
                }) ; 
    }



    public List<FeelTypeData> GetCosts()
    {
        return costManager.GetBuildingCost(BuildingData, false);
    }

    private List<FeelTypeData> GetCosts(bool consume)
    {
        return costManager.GetBuildingCost(BuildingData, consume);
    }

    public bool HasEnoughMoney()
    {
        var prices = GetCosts();

        foreach (FeelTypeData price in prices)
        {
            if (!price.HasEnoughFeels())
                return false;
        }
        return true;
    }

    public void RemoveFeelCost()
    {
        var prices = GetCosts(true);

        foreach (FeelTypeData price in prices)
        {
            price.Pay();
        }
    }

    public void PlacedBuilding()
    {
        isPlaced = true;
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
        if (lerpAlpha > 0.5f)
            lerpAlpha = 0;
    }

    /// <summary>
    /// Get the center of the building, base on it size in tile coordinate
    /// </summary>
    public Vector3 GetRootCoordinate()
    {
        return new Vector3((minimumX * 5 + maximumX * 5) / 2, 0, (minimumY * 5 - maximumY * 5)/2) + this.transform.position;
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
