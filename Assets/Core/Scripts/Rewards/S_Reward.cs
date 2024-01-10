using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_Reward : ScriptableObject
{
    public string Description = string.Empty;
    public abstract void GetReward();
}
