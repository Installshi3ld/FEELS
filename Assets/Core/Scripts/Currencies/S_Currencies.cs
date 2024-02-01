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
    public int Amount
    {
        get { return amount; } //read
        private set
        {
            amount = value;

            if(amount < 0) amount = 0;

            if (OnRefreshUi != null)
            {
                OnRefreshUi();
            }
        }

    }

    public Sprite image;
    public void AddAmount(float addAmount)
    {
        if (addAmount - Mathf.Floor(addAmount) >= .5f)
        {
            Amount += Mathf.CeilToInt(addAmount);

        }

        else
            Amount += Mathf.FloorToInt(addAmount);

    }
    public void RemoveAmount(float removeAmount)
    {
        //Debug.Log("CURRENCY DICREASED");
        if (removeAmount - Mathf.Floor(removeAmount) >= .5f)
        {
            Amount -= Mathf.CeilToInt(removeAmount);
        }

        else
            Amount -= Mathf.FloorToInt(removeAmount);
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }

    public bool HasEnoughFeels(int _amount)
    {
        return _amount <= amount;
    }


}
