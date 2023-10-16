using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tets : MonoBehaviour
{
    public S_FeelsScriptableObject feels;

    public void TestFunction()
    {
        feels.IncreaseFeels(3);
    }
}
