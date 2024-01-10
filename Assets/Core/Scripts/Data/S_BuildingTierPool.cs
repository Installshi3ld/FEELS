using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_BuildingTierPoolData", menuName = "Data/SO_BuildingTierPoolData")]
public class S_BuildingTierPool : ScriptableObject
{
    public List<GameObject> Building = new List<GameObject>();
}
