using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRequirement
{
    string NarrativeDescription { get; set; }
    string ConstraintDescription { get; set; }
    bool HasBeenFulfilled { get; set; }
    bool CheckIsRequirementFulfilled();
}
