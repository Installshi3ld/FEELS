using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Create new Event Timer (one ref)", menuName = "SingletonContainer/EventTimer")]
public class S_EventTimer : ScriptableObject
{
    [NonSerialized] public float eventTimerState;
    [NonSerialized] public float maxTime;

    [System.NonSerialized] public UnityEvent TimerChangedEvent;
    [System.NonSerialized] public UnityEvent MaxChangedEvent;
    public float EventTimerState
    {
        get { return eventTimerState; } //read
        set
        {
            eventTimerState = value;

            if(TimerChangedEvent != null)
            {
                TimerChangedEvent.Invoke();
            }

        }

    }

    public float MaxTime
    {
        get { return maxTime; } //read
        set
        {
            maxTime = value;

            if(MaxChangedEvent != null)
            {
                MaxChangedEvent.Invoke();   
            }
        }

    }

    public void StartTimerOver()
    {
        eventTimerState = 0;
    }


}
