using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Life Cycle Event")]
public class S_LifeCycle : ScriptableObject
{
    // Algo si j'ai besoin d'utiliser à ce moment là je check si il existe. Si non j'appelle la fonction qui le crée.


    public string description;
    [SerializeField]
    private List<S_EmotionScriptableObject> emotionsToIncrease = new List<S_EmotionScriptableObject>();
    private List<int> howMuchDecrease = new List<int>();
    
    
    private List<S_EmotionScriptableObject> emotionsToDecrease = new List<S_EmotionScriptableObject>();
    private List<int> howMuchIncrease = new List<int>();

    public Dictionary<S_EmotionScriptableObject, int> dictEmotionsToIncrease = new Dictionary<S_EmotionScriptableObject, int>();
    public Dictionary<S_EmotionScriptableObject, int> dictEmotionsToDecrease = new Dictionary<S_EmotionScriptableObject, int>();

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

    public Dictionary<S_EmotionScriptableObject, int> ToDictionnary(List<S_EmotionScriptableObject> keys, List<int> values)
    {
        Dictionary<S_EmotionScriptableObject, int> dictionary = new Dictionary<S_EmotionScriptableObject, int>();

        if(keys != null && values != null && keys.Count == values.Count)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
                Debug.Log(keys[i]);
            }
        }

        return dictionary;
    }
}
