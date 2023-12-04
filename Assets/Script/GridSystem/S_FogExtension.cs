using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FogExtension : MonoBehaviour
{
    public int radius = 7;
    public float delayBetweenFogUnlock = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UnlockFogTile());
    }

    IEnumerator UnlockFogTile()
    {
        S_Building buildingScript = GetComponentInParent<S_Building>();
        Vector3 tmpCoordinate = Grid.GetRandomTileAroundOtherOne(Grid.getIndexbasedOnPosition(this.transform.position), radius, false);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            print("efz");
            if (buildingScript.isPlaced)
            {

                if (tmpCoordinate != Vector3.zero)
                {
                    Vector2Int tmpIndex = Grid.getIndexbasedOnPosition(tmpCoordinate);
                    Grid.fogGridsUsageStatement[tmpIndex.x][tmpIndex.y] = false;
                    Destroy(Grid.fogGameObjects[tmpIndex.x][tmpIndex.y]);

                }
                tmpCoordinate = Grid.GetRandomTileAroundOtherOne(Grid.getIndexbasedOnPosition(this.transform.position), radius, false);
            }
        } 
    }
}
