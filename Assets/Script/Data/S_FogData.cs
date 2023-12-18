using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_FogData", menuName = "Data/SO_FogData")]
public class S_FogData : SerializedScriptableObject
{
    public List<List<bool>> fogGridsUsageStatement;
}
