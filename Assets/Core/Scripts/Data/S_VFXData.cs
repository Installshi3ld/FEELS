using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_VFXDataBuilding", menuName = "Data/SO_VFXDataBuilding")]
public class S_VFXData : SerializedScriptableObject
{
    [SerializeField] private GameObject EndOfConstruction;

    public GameObject GetVFXEndOfConstruction()
    {
        return EndOfConstruction;
    }
}
