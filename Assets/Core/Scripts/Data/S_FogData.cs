using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_FogData", menuName = "Data/SO_FogData")]
public class S_FogData : ScriptableObject
{
    public void Init(int tileAmount)
    {
        fogGridsUsageStatement = S_StaticFunc.Create2DimensionalList(tileAmount, () => false);
    }
    public List<List<bool>> fogGridsUsageStatement;
}
