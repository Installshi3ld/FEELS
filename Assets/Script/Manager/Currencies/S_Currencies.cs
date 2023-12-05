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


    public enum FeelType
    {
        None = 0,
        Joy,
        Anger,
        Sad,
        Fear,
    }

    /// <summary>
    /// This type is for building, to know which case activate bonus
    /// </summary>
    [SerializeField]
    public FeelType feelType;

    public int amount = 0;
    public Sprite image;
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
    public void RemoveAmount(float removeAmount)
    {
        if (removeAmount - Mathf.Floor(removeAmount) >= .5f)
        {
            amount -= Mathf.CeilToInt(removeAmount);
        }

        else
            amount -= Mathf.FloorToInt(removeAmount);

        if (OnRefreshUi != null)
        {
            OnRefreshUi();
        }
    }

    public bool HasEnoughFeels(int _amount)
    {
        return _amount <= amount;
    }
}
