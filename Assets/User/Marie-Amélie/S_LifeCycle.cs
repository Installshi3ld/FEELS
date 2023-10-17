using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Life Cycle Event")]
public class S_LifeCycle : ScriptableObject
{
    public string description;

    [SerializeField]
    public Dictionary<S_EmotionScriptableObject, int> emotionsToIncrease = new Dictionary<S_EmotionScriptableObject, int>();
    public Dictionary<S_EmotionScriptableObject, int> emotionsToDecrease = new Dictionary<S_EmotionScriptableObject, int>();

    public void LifeCycleEffectIncrease(Dictionary<S_EmotionScriptableObject, int> emotionsToActOn)
    {
        foreach (var emotion in emotionsToActOn)
        {
            S_EmotionScriptableObject emotionScriptableObject = emotion.Key;
            int amount = emotion.Value;
            emotionScriptableObject.EmotionAmount += amount;
        }
    }

    public void LifeCycleEffectDecrease(Dictionary<S_EmotionScriptableObject, int> emotionsToActOn)
    {
        foreach (var emotion in emotionsToActOn)
        {
            S_EmotionScriptableObject emotionScriptableObject = emotion.Key;
            int amount = emotion.Value;
            emotionScriptableObject.EmotionAmount -= amount;
        }
    }
}
