using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Gauge : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public S_FeelsScriptableObject playerFeels;

    private void OnEnable()
    {
        playerFeels.feelsAmountChangeEvent.AddListener(SetSliderValue);
        SetSliderValue();
    }

    private void OnDisable()
    {
        playerFeels.feelsAmountChangeEvent.RemoveListener(SetSliderValue);
    }

    public void SetSliderValue()
    {
        slider.value = playerFeels.FeelsAmount;
    }
}
