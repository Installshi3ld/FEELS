using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_Disaster : ScriptableObject, IDisaster
{
    private string description = string.Empty;
    public string Description
    {
        get
        {
            return this.description;
        }
        set
        {
            this.description = value;
        }
    }

    public abstract void ProvoqueDisaster();
}
