using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class S_Requirement : ScriptableObject
{
    public Sprite spriteImage;

    public string NarrativeDescription = string.Empty;

    public string ConstraintDescription = string.Empty;

    public int numberOfTurnToFulfill;

    [NonSerialized]
    public bool HasBeenFulfilled = false;

    public List<S_Disaster> LinkedDisaster = new List<S_Disaster>();

    public List<S_Reward> LinkedRewards = new List<S_Reward>();
    public abstract bool CheckIsRequirementFulfilled();
    public abstract void DoSomethingAtFirst();
}