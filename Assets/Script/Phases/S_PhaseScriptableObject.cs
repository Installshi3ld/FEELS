using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Phase")]
public class S_PhaseScriptableObject : ScriptableObject
{
    public string nameOfPhase;

    public List<S_Requirement> requirements = new List<S_Requirement>();
    public List<S_LifeExperienceScriptableObject> lifeExperiences = new List<S_LifeExperienceScriptableObject>();

    public S_PhaseScriptableObject MakeCopy()
    {
        //return this.MemberwiseClone() as S_PhaseScriptableObject;

        S_PhaseScriptableObject clone = CreateInstance<S_PhaseScriptableObject>();

        for (int i = 0; i < this.requirements.Count; i++)
        {
            clone.requirements.Add(this.requirements[i].MakeCopy());
        }
        for (int i = 0; i < this.lifeExperiences.Count; i++)
        {
            clone.lifeExperiences.Add(this.lifeExperiences[i].MakeCopy());
        }

        return clone;
    }

}
