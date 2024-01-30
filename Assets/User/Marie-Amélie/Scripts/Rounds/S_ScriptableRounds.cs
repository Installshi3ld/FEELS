using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Rounds", menuName = "SingletonContainer/Rounds")]
public class S_ScriptableRounds : ScriptableObject
{
    [SerializeField] private int numberOfRoundToSwitchEvent;
    private int currentRound;

    public Action OnChangedRound;

    public void SwitchRound()
    {
        currentRound++;
        CheckChangeEvent();

        OnChangedRound?.Invoke(); //if not null
    }

    private void CheckChangeEvent()
    {
        //
    }
}
