using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Event")]
public class S_EventScriptableObject : ScriptableObject
{
    //algo that check if exist at the moment I need it. If not create it
    public string description;

    [SerializeField] //INCREASE
    private List<S_EmotionScriptableObject> emotionsToIncrease = new List<S_EmotionScriptableObject>();
    [SerializeField]
    private List<int> howMuchIncrease = new List<int>();

    [SerializeField] //DECREASE
    private List<S_EmotionScriptableObject> emotionsToDecrease = new List<S_EmotionScriptableObject>();
    [SerializeField]
    private List<int> howMuchDecrease = new List<int>();
    
    // DICTIONARIES
    public Dictionary<S_EmotionScriptableObject, int> dictEmotionsToIncrease = new Dictionary<S_EmotionScriptableObject, int>();
    public Dictionary<S_EmotionScriptableObject, int> dictEmotionsToDecrease = new Dictionary<S_EmotionScriptableObject, int>();

    public void EventEffectIncrease(Dictionary<S_EmotionScriptableObject, int> emotionsToActOn)
    {
        foreach (var emotion in emotionsToActOn)
        {
            S_EmotionScriptableObject emotionScriptableObject = emotion.Key;
            int amount = emotion.Value;
            emotionScriptableObject.EmotionAmount += amount;
        }
    }

    public void EventEffectDecrease(Dictionary<S_EmotionScriptableObject, int> emotionsToActOn)
    {
        foreach (var emotion in emotionsToActOn)
        {
            S_EmotionScriptableObject emotionScriptableObject = emotion.Key;
            int amount = emotion.Value;
            emotionScriptableObject.EmotionAmount -= amount;
        }
    }

    public Dictionary<S_EmotionScriptableObject, int> ToDictionary(List<S_EmotionScriptableObject> keys, List<int> values)
    {
        Dictionary<S_EmotionScriptableObject, int> dictionary = new Dictionary<S_EmotionScriptableObject, int>();

        if(keys != null && values != null && keys.Count == values.Count)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
        }

        return dictionary;
    }

    public void applyEvent()
    {
        if(dictEmotionsToDecrease != null && dictEmotionsToIncrease != null)
        {
            dictEmotionsToIncrease = ToDictionary(emotionsToIncrease, howMuchIncrease);
            dictEmotionsToDecrease = ToDictionary(emotionsToDecrease, howMuchDecrease);
        }

        EventEffectIncrease(dictEmotionsToIncrease);
        EventEffectDecrease(dictEmotionsToDecrease);
    }

}

[Serializable]
public struct EmotionsModifyer
{
    public S_Currencies currency;
    public int amount;
}