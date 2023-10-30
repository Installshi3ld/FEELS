using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Gauge : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public S_EmotionScriptableObject playerEmotion;

    private void OnEnable()
    {
        playerEmotion.emotionAmountChangeEvent.AddListener(SetSliderValue);
        SetSliderValue();
    }

    private void OnDisable()
    {
        playerEmotion.emotionAmountChangeEvent.RemoveListener(SetSliderValue);
    }

    public void SetSliderValue()
    {
        slider.value = playerEmotion.EmotionAmount;
    }
}
