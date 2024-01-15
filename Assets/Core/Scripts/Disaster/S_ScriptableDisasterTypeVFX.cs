using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "SO_SingleVFXDisaster", menuName = "SingletonContainer/VFXDisasters")]
public class S_ScriptableDisasterTypeVFX : ScriptableObject
{
    public List<FeelTypeMatchVFX> typeRequirements;
}

[Serializable]
public struct FeelTypeMatchVFX
{
    public FeelType feelType;
    public S_VFXElement vfx;
}


