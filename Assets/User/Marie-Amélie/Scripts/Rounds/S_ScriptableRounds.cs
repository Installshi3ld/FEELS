using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Rounds", menuName = "SingletonContainer/Rounds")]
public class S_ScriptableRounds : ScriptableObject
{
    [SerializeField] private int numberOfRoundToSwitchEvent;
    private int currentRound;

    public Action OnChangedTurn;
    public Action OnChangedRound;

    public int CurrentRound
    {
        get
        {
            return currentRound;
        }
        private set
        {
            currentRound = value;

            OnChangedTurn?.Invoke(); //if not null
        }
    }
    public void SwitchRound()
    {
        currentRound++;
        CheckChangeEvent();
    }

    private void CheckChangeEvent()
    {
        if(currentRound % numberOfRoundToSwitchEvent == 0)
        {
            OnChangedRound?.Invoke();
        }
    }
}
