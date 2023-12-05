using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class S_Requirement : ScriptableObject, IRequirement
{
    private string narrativeDescription = string.Empty;

    public string NarrativeDescription
    {
        get
        {
            return this.narrativeDescription;
        }
        set
        {
            this.narrativeDescription = value;
        }
    }

    private string constraintDescription = string.Empty;

    public string ConstraintDescription
    {
        get
        {
            return this.constraintDescription;
        }
        set
        {
            this.constraintDescription = value;
        }
    }

    private bool hasBeenFulfilled = false;

    public bool HasBeenFulfilled
    {
        get
        {
            return this.hasBeenFulfilled;
        }
        set
        {
            this.hasBeenFulfilled = value;
        }
    }

    public List<S_Disaster> linkedDisaster = new List<S_Disaster>();

    public List<S_Disaster> LinkedDisaster
    {
        get
        {
            return this.linkedDisaster;
        }
        set
        {
            this.linkedDisaster = value;
        }
    }

    public abstract bool CheckIsRequirementFulfilled();

}