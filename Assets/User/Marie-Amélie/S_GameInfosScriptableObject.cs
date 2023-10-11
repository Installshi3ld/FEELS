using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Player emotions")]
public class S_GameInfosScriptableObject : ScriptableObject
{
    public int joyFeels, angerFeels, sadnessFeels, fearFeels;
    public int[] emotions;

    // People subscribe to those events to get notified of joy, angel, sadness or fear changes.

    [System.NonSerialized]
    public UnityEvent emotionFeelsChangeEvent;

    public float currentMood;

    public float currency;

    private void OnEnable()
    {
        joyFeels = 0;
        angerFeels = 0;
        sadnessFeels = 0;
        fearFeels = 0;

        emotions = new int[] { joyFeels, angerFeels, sadnessFeels, fearFeels };

        if (emotionFeelsChangeEvent == null)
        {
            emotionFeelsChangeEvent = new UnityEvent();
        }

    }

    public void DecreaseEmotions(int indexEmotion, int amount)
    {
        if(indexEmotion < 4 && indexEmotion > 0)
        {

        }
        emotions[indexEmotion] -= amount;
        emotionFeelsChangeEvent.Invoke();
    }

    public void IncreaseEmotions(int indexEmotion, int amount)
    {
        emotions[indexEmotion] += amount;
        emotionFeelsChangeEvent.Invoke();
    }

}
