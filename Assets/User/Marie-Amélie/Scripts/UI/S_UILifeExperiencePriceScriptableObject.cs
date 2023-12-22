using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_UILifeExperiencePriceScriptableObject", menuName = "SingletonContainer/UILifeExperiencePriceScriptableObject")]
public class S_UILifeExperiencePriceScriptableObject : ScriptableObject
{
    public delegate void RefreshFromLifeExperience(S_LifeExperienceScriptableObject lifeExp);
    public event RefreshFromLifeExperience UpdatePriceUILifeExp;


    public void CallDelegate_UpdatePriceUILifeExp(S_LifeExperienceScriptableObject lifeExp)
    {
        UpdatePriceUILifeExp?.Invoke(lifeExp);
    }
}
