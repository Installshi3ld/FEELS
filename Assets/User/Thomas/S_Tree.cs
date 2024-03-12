using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tree : MonoBehaviour
{
    [SerializeField] private S_GridData _gridData;

    Vector2Int[] coordinateToUse = { 
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(0, -1),
        new Vector2Int(0, 0),
        new Vector2Int(0, 1),
        new Vector2Int(1, -1),
        new Vector2Int(1, 0),
        new Vector2Int(1, 1),

    };

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.06f);
        Vector2Int tmpIndex = _gridData.GetIndexbasedOnPosition(gameObject.transform.position);

        for (int i = 0; i < coordinateToUse.Length; i++)
        {
            _gridData.SetTileUsed(tmpIndex.x + coordinateToUse[i].x, tmpIndex.y + coordinateToUse[i].y);
        }
    }
}
