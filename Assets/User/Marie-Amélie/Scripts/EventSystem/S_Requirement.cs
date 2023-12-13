using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public abstract class S_Requirement : ScriptableObject
{
    public string NarrativeDescription = string.Empty;

    public string ConstraintDescription = string.Empty;

    [NonSerialized]
    public bool HasBeenFulfilled = false;

    public List<S_Disaster> LinkedDisaster = new List<S_Disaster>();
    public abstract bool CheckIsRequirementFulfilled();

}