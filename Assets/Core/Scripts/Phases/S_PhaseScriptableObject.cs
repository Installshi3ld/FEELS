using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Phase")]
public class S_PhaseScriptableObject : ScriptableObject
{
    public string nameOfPhase;
    public int numberOfRequirementToFulfillToSwitchPhase;

    public List<S_Requirement> requirements = new List<S_Requirement>();
    public List<S_LifeExperienceScriptableObject> lifeExperiences = new List<S_LifeExperienceScriptableObject>();
}
