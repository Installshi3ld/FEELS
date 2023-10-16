using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Scriptable Object", menuName = "EMOTION")]
public class S_EmotionScriptableObject : ScriptableObject
{
    public string emotionType;

    [System.NonSerialized]
    public int emotionAmount;

    [System.NonSerialized]
    public UnityEvent emotionAmountChangeEvent;

    public int EmotionAmount
    {
        get { return emotionAmount; } //read
        set
        {
            if (value > 100)
            {
                emotionAmount = 100;
            }
            else
            {
                emotionAmount = value;
            }

            if( emotionAmountChangeEvent != null )
            {
                emotionAmountChangeEvent.Invoke();
            }
            
        }

    }
    private void OnEnable()
    {
        EmotionAmount = 0;

        if (emotionAmountChangeEvent == null)
        {
            emotionAmountChangeEvent = new UnityEvent();
        }
    }

    public void IncreaseEmotion(int amount)
    {
        EmotionAmount += amount;
    }

    public void DecreaseEmotion(int amount)
    {
        EmotionAmount -= amount;
    }
}
