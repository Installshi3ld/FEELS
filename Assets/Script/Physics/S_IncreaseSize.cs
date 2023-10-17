using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_IncreaseSize : MonoBehaviour
{
    public void IncreaseSize(GameObject toIncrease, float XIncrease, float YIncrease)
    {
        toIncrease.transform.localScale += new Vector3(XIncrease, 0, 0);
        toIncrease.transform.localScale += new Vector3(0, YIncrease, 0);
    }
}
