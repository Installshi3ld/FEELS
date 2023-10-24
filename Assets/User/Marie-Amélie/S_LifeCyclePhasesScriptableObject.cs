using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Life Cycle Phase")]
public class S_LifeCyclePhasesScriptableObject : ScriptableObject
{
    public string nameOfPhase;

    public float phaseDuration;

    public List<S_LifeCycleEventsScriptableObject> events = new List<S_LifeCycleEventsScriptableObject>();
}
