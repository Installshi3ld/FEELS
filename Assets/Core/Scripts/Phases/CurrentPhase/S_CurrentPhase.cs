using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Current Phase")]
public class S_CurrentPhase : ScriptableObject
{
    [NonSerialized]
    public int phaseIndex;

    public int PhaseIndex
    {
        get { return phaseIndex; } //read
        set
        {
            phaseIndex = value;
        }
    }
    private void OnEnable()
    {
        phaseIndex = 0;
    }

}
