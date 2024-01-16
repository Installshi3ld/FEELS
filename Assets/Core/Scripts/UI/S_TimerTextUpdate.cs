using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class S_TimerTextUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] S_EventTimer timer;

    private void OnEnable()
    {
        timer.TimerChangedEvent.AddListener(UpdateTimerValue);
    }

    private void OnDisable()
    {
        timer.TimerChangedEvent.RemoveListener(UpdateTimerValue);
    }

    private void UpdateTimerValue()
    {
        textField.text = ((int)Math.Floor(timer.maxTime - timer.eventTimerState) + " seconds left");
    }
}
