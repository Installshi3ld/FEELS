using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class S_FireLifeExperience : MonoBehaviour
{

    public GameObject smallFire;

    List<GameObject> allFire = new List<GameObject>();
    List<Vector2Int> gridUsage = new List<Vector2Int>();

    private void Start()
    {
        this.transform.position = Grid.GetRandomTileInGrid();
        StartCoroutine(FlamePropagation());
        
        Vector2Int tmpIndex = Grid.getIndexbasedOnPosition(this.transform.position);
        gridUsage.Add(tmpIndex);
        Grid.SetTileUsed(tmpIndex.x, tmpIndex.y);
    }
    

    IEnumerator FlamePropagation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector3 tmpCoordinate = Grid.GetRandomTileAroundOtherOne(Grid.getIndexbasedOnPosition(this.transform.position), 3);
            if (!Grid.gridsUsageStatement[Grid.getIndexbasedOnPosition(tmpCoordinate).x][Grid.getIndexbasedOnPosition(tmpCoordinate).y])
            {
                allFire.Add(GameObject.Instantiate(smallFire, tmpCoordinate, Quaternion.identity));

                Vector2Int tmpIndex = Grid.getIndexbasedOnPosition(tmpCoordinate);
                gridUsage.Add(tmpIndex);
                Grid.SetTileUsed(tmpIndex.x, tmpIndex.y);
            }
            
        }
        
    }

    private void OnDestroy()
    {
        foreach(Vector2Int element in gridUsage)
        {
            Grid.RemoveTileUsed(element.x, element.y);
        }
        foreach(GameObject Object in allFire)
        {
            Destroy(Object);
        }
    }
}
