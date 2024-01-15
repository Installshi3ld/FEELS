using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class S_StartVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect vfx;

    void Start()
    {
        vfx.pause = true;
    }
}
