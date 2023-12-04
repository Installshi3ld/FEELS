using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class S_RequirementAbstract : ScriptableObject, IRequirement
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

    public abstract bool CheckIsRequirementFulfilled();

}

public class S_Requirement : S_RequirementAbstract
{
    public override bool CheckIsRequirementFulfilled()
    {
        return false;
    }

    public S_Requirement MakeCopy()
    {
        S_Requirement clone = CreateInstance<S_Requirement>();

        clone.NarrativeDescription = NarrativeDescription;
        clone.ConstraintDescription = ConstraintDescription;
        clone.HasBeenFulfilled = HasBeenFulfilled;

        return clone;
    }

}
