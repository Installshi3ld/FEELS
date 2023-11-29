using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRequirement
{
    List<S_Disaster> LinkedDisaster { get; set; } 
    string NarrativeDescription { get; set; }
    string ConstraintDescription { get; set; }
    bool HasBeenFulfilled { get; set; }
    bool CheckIsRequirementFulfilled();
}
