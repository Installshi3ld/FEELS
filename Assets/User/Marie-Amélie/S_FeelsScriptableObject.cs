using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Scriptable Object", menuName = "FEELS")]
public class S_FeelsScriptableObject : ScriptableObject
{
    public string feelsType;

    [System.NonSerialized]
    public int feelsAmount;

    [System.NonSerialized]
    public UnityEvent feelsAmountChangeEvent;

    public int FeelsAmount
    {
        get { return feelsAmount; } //read
        set
        {
            if (value > 100)
            {
                feelsAmount = 100;
            }
            else
            {
                feelsAmount = value;
            }

            feelsAmountChangeEvent.Invoke();
        }

    }
    private void OnEnable()
    {
        feelsAmount = 0;

        if (feelsAmountChangeEvent == null)
        {
            feelsAmountChangeEvent = new UnityEvent();
        }
    }

    public void IncreaseFeels(int amount)
    {
        FeelsAmount += amount;
    }

    public void DecreaseFeels(int amount)
    {
        FeelsAmount -= amount;
    }
}
