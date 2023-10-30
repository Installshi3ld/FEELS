using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Event")]
public class S_EventScriptableObject : ScriptableObject
{
    // Algo si j'ai besoin d'utiliser � ce moment l� je check si il existe. Si non j'appelle la fonction qui le cr�e.

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

    public S_EventScriptableObject MakeCopy()
    {
        S_EventScriptableObject clone = CreateInstance<S_EventScriptableObject>();

        for (int i = 0; i < this.emotionsToIncrease.Count; i++)
        {
            clone.emotionsToIncrease.Add(this.emotionsToIncrease[i]);
        }

        for (int i = 0; i < this.howMuchIncrease.Count; i++)
        {
            clone.howMuchIncrease.Add(this.howMuchIncrease[i]);
        }

        for (int i = 0; i < this.emotionsToDecrease.Count; i++)
        {
            clone.emotionsToDecrease.Add(this.emotionsToDecrease[i]);
        }

        for (int i = 0; i < this.howMuchDecrease.Count; i++)
        {
            clone.howMuchDecrease.Add(this.howMuchDecrease[i]);
        }

        return clone;
    }
}
