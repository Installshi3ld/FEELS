using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StartGameFeels : MonoBehaviour
{
    [SerializeField] List<FeelsAmount> feels = new List<FeelsAmount>();

    private void Start()
    {
        foreach (var feel in feels)
        {
            feel.currency.SetAmount(feel.amount);
        }
    }
}

[Serializable]
public struct FeelsAmount
{
    public S_Currencies currency;
    public int amount;
}