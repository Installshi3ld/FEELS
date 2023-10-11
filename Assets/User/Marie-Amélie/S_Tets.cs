using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tets : MonoBehaviour
{

    public S_GameInfosScriptableObject playerInfos;

    public void TestFunction()
    {
        playerInfos.IncreaseEmotions(0, 30);
    }
}
