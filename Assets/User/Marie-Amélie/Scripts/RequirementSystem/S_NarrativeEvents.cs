using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_NarrativeEvent", menuName = "Requirements/NarrativeEvents")]
public class S_NarrativeEvents : ScriptableObject
{
    public List<NarrativeContainers> narrativeEventPhases;
    private int currentPhaseIndex = 0;
}


[Serializable]
public struct NarrativeContainers
{
    public string description;
    public S_Requirement requirement;
    public int secondsBetweenNextRequirement;
}
