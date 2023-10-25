using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "SO_Feels", menuName = "Currencies/Feels", order = 1)]
public class S_Currencies : ScriptableObject
{
    public int amount = 0;
    public void AddAmount(float addAmount)
    {
        if (addAmount - Mathf.Floor(addAmount) >= .5f)
        {
            amount += Mathf.CeilToInt(addAmount);
            Debug.Log(Mathf.CeilToInt(addAmount));
        }

        else
            amount += Mathf.FloorToInt(addAmount);

        S_FeelsUI.RefreshUI();
    }
}
