using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;

public class S_LifeExperience : MonoBehaviour
{
    public GameObject smallFire;
    public GameObject wonderBuilding;

    List<GameObject> allFire = new List<GameObject>();
    List<Vector2Int> gridUsage = new List<Vector2Int>();

    public float delayBeforeFlamePropagation = 5;

    [SerializeField] private S_GridData _gridData = default(S_GridData);
    private void Awake()
    {
        wonderBuilding.GetComponent<S_Building>().buildingDataSO.isPlacedAnimation = true;
    }
    private void Start()
    {
        S_Building building = wonderBuilding.GetComponent<S_Building>();
        this.transform.position = _gridData.GetRandomTileInGrid(building.buildingDataSO.tilesCoordinate) + new Vector3(0, 50, 0);

        if (this.transform.position.y <= -500)
            Destroy(this);

        
        Vector2Int tmpIndex = _gridData.GetIndexbasedOnPosition(this.transform.position);

        _gridData.SetTileUsed(tmpIndex.x, tmpIndex.y);
        foreach (Vector2Int element in building.buildingDataSO.tilesCoordinate)
        {
            _gridData.SetTileUsed(tmpIndex.x + element.x, tmpIndex.y + element.y);
        }
        
    }

    bool isFlamePropagation = false;
    private void Update()
    {
        if(this.transform.position.y > 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 10f * Time.deltaTime, this.transform.position.z);
        }
        else if (!isFlamePropagation)
        {
            StartCoroutine(FlamePropagation());
            isFlamePropagation =true;
        }
    }

    void SpawnWonder()
    {
        GameObject wonder = GameObject.Instantiate(wonderBuilding, this.transform.position, Quaternion.identity);
        wonder.GetComponent<S_Building>().buildingDataSO.PlacedBuilding();
        Destroy(this.gameObject);
    }

    IEnumerator FlamePropagation()
    {
        yield return new WaitForSeconds(delayBeforeFlamePropagation);
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector3 tmpCoordinate = _gridData.GetRandomTileAroundOtherOne(_gridData.GetIndexbasedOnPosition(this.transform.position), 3, false);
            if(tmpCoordinate != Vector3.zero)
            {
                if (!_gridData.gridsUsageStatement[_gridData.GetIndexbasedOnPosition(tmpCoordinate).x][_gridData.GetIndexbasedOnPosition(tmpCoordinate).y].statement)
                {
                    allFire.Add(GameObject.Instantiate(smallFire, tmpCoordinate, Quaternion.identity));

                    Vector2Int tmpIndex = _gridData.GetIndexbasedOnPosition(tmpCoordinate);
                    gridUsage.Add(tmpIndex);
                    _gridData.SetTileUsed(tmpIndex.x, tmpIndex.y);
                }

            }
            
        }
        
    }
    private void OnDisable()
    {
        foreach (Vector2Int element in gridUsage)
        {
            _gridData.RemoveTileUsed(element.x, element.y);
        }
        foreach (GameObject Object in allFire)
        {
            Destroy(Object);
        }

        SpawnWonder();
    }
}
