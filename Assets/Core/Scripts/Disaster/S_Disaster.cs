using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_Disaster : ScriptableObject
{
    public string Description = string.Empty;
    public abstract void ProvoqueDisaster();
}
