using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Rounds", menuName = "SingletonContainer/Rounds")]
public class S_ScriptableRounds : ScriptableObject
{
    [SerializeField] public int numberOfRoundToSwitchEvent;
    private int _currentRound;

    [SerializeField] private int _actionPoints;

    public Action OnChangedTurn;
    public Action OnChangedRound;
    public Action OnActionPointRemoved;

    public int CurrentRound
    {
        get
        {
            return _currentRound;
        }
        set
        {
            _currentRound = value;
            OnChangedTurn?.Invoke(); //if not null
        }
    }

    private void OnEnable()
    {
        _currentRound = 0;
    }

    public int GetNumberOfRounds()
    {
        return _currentRound;
    }
    public void SwitchRound()
    {
        CurrentRound++;
        CheckChangeEvent();
    }

    public bool TryRemoveActionPoints(int toRemove)
    {
        if(_actionPoints - toRemove < 0)
        {
            return false;
        }
        else
        {
            _actionPoints -= toRemove;
            OnActionPointRemoved?.Invoke();
            return true;
        }
    }

    private void CheckChangeEvent()
    {
        if(_currentRound == numberOfRoundToSwitchEvent)
        {
            _currentRound = 0;
            Debug.Log("calling delegate");
            OnChangedRound?.Invoke();
        }
    }

    public int GetRoundsLeft()
    {
        return numberOfRoundToSwitchEvent - CurrentRound;
    }
}
