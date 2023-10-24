using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Phase")]
public class S_PhaseScriptableObject : ScriptableObject
{
    public string nameOfPhase;

    public float phaseDuration;

    public List<S_EventScriptableObject> events = new List<S_EventScriptableObject>();
}
