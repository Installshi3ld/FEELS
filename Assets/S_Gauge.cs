using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Gauge : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public S_EmotionScriptableObject playerFeels;

    private void OnEnable()
    {
        playerFeels.emotionAmountChangeEvent.AddListener(SetSliderValue);
        SetSliderValue();
    }

    private void OnDisable()
    {
        playerFeels.emotionAmountChangeEvent.RemoveListener(SetSliderValue);
    }

    public void SetSliderValue()
    {
        slider.value = playerFeels.EmotionAmount;
    }
}
