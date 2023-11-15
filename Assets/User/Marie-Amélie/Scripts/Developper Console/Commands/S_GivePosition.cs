using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GivePosition : MonoBehaviour
{
    private void OnEnable()
    {
        //AddListener Unity Eveent 
    }
    private void giveObjectPosition(GameObject go, float howLon)
    {
        StartCoroutine(givePositionEveryXSecond(go, howLon));
    }
    private IEnumerator givePositionEveryXSecond(GameObject go, float howLong)
    {
        while(howLong >= 0)
        {
            Debug.Log("Working ?");
            Debug.Log(go.transform.position);
            howLong--;

            yield return new WaitForSeconds(1f);
        }

    } 
}
