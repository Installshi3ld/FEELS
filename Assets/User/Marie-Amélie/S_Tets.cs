using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tets : MonoBehaviour
{
    public S_FeelsScriptableObject playerInfos;

    public void TestFunction()
    {
        playerInfos.IncreaseJoy(3);
    }
}
