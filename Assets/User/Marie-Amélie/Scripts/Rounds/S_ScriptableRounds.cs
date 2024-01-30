using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Rounds", menuName = "SingletonContainer/Rounds")]
public class S_ScriptableRounds : ScriptableObject
{
    [SerializeField] private int numberOfRoundToSwitchEvent;
    private int _currentRound;

    private int _actionPoints;

    public Action OnChangedTurn;
    public Action OnChangedRound;
    public Action OnActionPointRemoved;

    public int CurrentRound
    {
        get
        {
            return _currentRound;
        }
        private set
        {
            _currentRound = value;

            OnChangedTurn?.Invoke(); //if not null
        }
    }
    public void SwitchRound()
    {
        _currentRound++;
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
        if(_currentRound % numberOfRoundToSwitchEvent == 0)
        {
            OnChangedRound?.Invoke();
        }
    }
}
