using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Phase")]
public class S_PhaseScriptableObject : ScriptableObject
{
    public string nameOfPhase;

    public float phaseDuration;

    public List<S_EventScriptableObject> events = new List<S_EventScriptableObject>();

    public S_PhaseScriptableObject MakeCopy()
    {
        //return this.MemberwiseClone() as S_PhaseScriptableObject;

        S_PhaseScriptableObject clone = CreateInstance<S_PhaseScriptableObject>();

        /*for (int i = 0; i < this.events.Count-1; i++)
        {
            //clone.events[i] = this.events[i].MakeCopy();
            clone.events[i] = this.events[i];
        }*/


        return clone;
    }
}
