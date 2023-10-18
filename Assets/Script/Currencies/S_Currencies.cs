using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "SO_Feels", menuName = "Currencies/Feels", order = 1)]
public class S_Currencies : ScriptableObject
{
    public int amount = 0;

    void AddAmount(int _amount)
    {
        amount += _amount;
    }
}
