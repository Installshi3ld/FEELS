using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Phase")]
public class S_PhaseScriptableObject : ScriptableObject
{
    public string nameOfPhase;

    public List<S_EventScriptableObject> events = new List<S_EventScriptableObject>();

    public S_PhaseScriptableObject MakeCopy()
    {
        //return this.MemberwiseClone() as S_PhaseScriptableObject;

        S_PhaseScriptableObject clone = CreateInstance<S_PhaseScriptableObject>();

        for (int i = 0; i < this.events.Count; i++)
        {
            clone.events.Add(this.events[i].MakeCopy());
        }

        return clone;
    }
}
