using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FogExtension : MonoBehaviour
{
    public int radius = 7;
    public float delayBetweenFogUnlock = 3f;

    [SerializeField] private S_GridData _gridData;

    // Start is called before the first frame update
    void Start()
    {
        if (_gridData != null)
            StartCoroutine(UnlockFogTile());
        else
            Debug.LogWarning("S_GridData is missing in S_FogExtension", this);
    }

    IEnumerator UnlockFogTile()
    {
        S_Building buildingScript = GetComponentInParent<S_Building>();
        Vector3 tmpCoordinate = _gridData.GetRandomTileAroundOtherOne(_gridData.GetIndexbasedOnPosition(this.transform.position), radius, false);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (buildingScript.isPlaced)
            {

                if (tmpCoordinate != Vector3.zero)
                {
                    Vector2Int tmpIndex = _gridData.GetIndexbasedOnPosition(tmpCoordinate);
                    Grid.fogGridsUsageStatement[tmpIndex.x][tmpIndex.y] = false;
                    Destroy(Grid.fogGameObjects[tmpIndex.x][tmpIndex.y]);

                }
                tmpCoordinate = _gridData.GetRandomTileAroundOtherOne(_gridData.GetIndexbasedOnPosition(this.transform.position), radius, false);
            }
        } 
    }
}
