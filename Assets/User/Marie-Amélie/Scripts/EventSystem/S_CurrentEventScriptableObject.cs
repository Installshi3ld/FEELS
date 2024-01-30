using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SO_CurrentEvent", menuName = "SingletonContainer/CurrentEvent")]
public class S_CurrentEventScriptableObject : ScriptableObject
{
    public delegate void RefreshRequirement(S_Requirement currentR, float delay);
    public event RefreshRequirement OnChangingRequirement;

    private float delay = 0;

    private S_Requirement currentRequirement;
    public S_Requirement CurrentRequirement
    {
        get { return currentRequirement; } //read
        private set
        {
            currentRequirement = value;

            if(OnChangingRequirement != null)
            {
                OnChangingRequirement(currentRequirement, delay);
            }
        }
    }

    public void SetNewRequirement(S_Requirement newRequirement, float theDelay)
    {
        if(newRequirement != null)
        {
            delay = theDelay;
            CurrentRequirement = newRequirement;
        }
    }
}
