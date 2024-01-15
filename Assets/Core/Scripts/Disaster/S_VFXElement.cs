using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class S_VFXElement : ScriptableObject
{
    [SerializeField]
    public VisualEffectAsset effect;
    public abstract void Triggers();
}
