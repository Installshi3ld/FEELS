using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_UILifeExpDelegateScriptableObject", menuName = "SingletonContainer/UILifeExpDelegateScriptableObject")]
public class S_UILifeExpDelegateScriptableObject : ScriptableObject
{
    public delegate void RefreshFromLifeExperience(bool isLifeExperienceActive);
    public event RefreshFromLifeExperience OnLifeExperienceSpawned;

    private bool isLifeExperienceActive;

    // Public property with a getter and a setter
    public bool IsLifeExperienceActive
    {
        get
        {
            return isLifeExperienceActive;
        }
        private set
        {
            isLifeExperienceActive = value;

            if(OnLifeExperienceSpawned!=null) OnLifeExperienceSpawned.Invoke(value);

        }
    }
    public void SetLifeExperienceBool(bool isLifeExperienceActive)
    {
        IsLifeExperienceActive = isLifeExperienceActive;
    }
}
