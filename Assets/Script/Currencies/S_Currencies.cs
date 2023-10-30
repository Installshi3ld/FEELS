using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static S_Currencies;

[System.Serializable]
[CreateAssetMenu(fileName = "SO_Feels", menuName = "Currencies/Feels", order = 1)]
public class S_Currencies : ScriptableObject
{
    public delegate void RefreshUIDelegate();
    public event RefreshUIDelegate OnRefreshUi;

    public int amount = 0;
    public void AddAmount(float addAmount)
    {
        if (addAmount - Mathf.Floor(addAmount) >= .5f)
        {
            amount += Mathf.CeilToInt(addAmount);
        }

        else
            amount += Mathf.FloorToInt(addAmount);

        if (OnRefreshUi != null)
        {
            OnRefreshUi();
        }
    }
}
