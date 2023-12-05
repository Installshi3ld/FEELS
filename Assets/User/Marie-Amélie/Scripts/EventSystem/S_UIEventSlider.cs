using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UIEventSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] S_EventTimer timer;

    private void OnEnable()
    {
        timer.MaxChangedEvent.AddListener(SetMaxSliderValue);
        timer.TimerChangedEvent.AddListener(UpdateSliderValue);
    }

    private void OnDisable()
    {
        timer.MaxChangedEvent.RemoveListener(SetMaxSliderValue);
        timer.TimerChangedEvent.RemoveListener(UpdateSliderValue);
    }

    private void UpdateSliderValue()
    {
        if (slider != null)
        {
            slider.value = timer.eventTimerState;
        }
    }

    private void SetMaxSliderValue()
    {
        if (slider != null)
        {
            slider.maxValue = timer.maxTime;
        }
    }
}
