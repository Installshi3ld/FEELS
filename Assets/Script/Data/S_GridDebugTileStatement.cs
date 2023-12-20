using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_GridDebugTileStatement", menuName = "Data/Grid/SO_DebugTileStatement")]
public class S_GridDebugTileInt : S_Data<int>
{
    
    public int maxValue;
    public void IncrementValue()
    {
        SetValue(value + 1);
        if (value >= maxValue)
            SetValue(0);
    }
}
