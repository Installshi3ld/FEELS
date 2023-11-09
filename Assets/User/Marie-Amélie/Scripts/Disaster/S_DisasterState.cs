using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Disaster State")]
public class S_DisasterState : ScriptableObject
{
    [System.NonSerialized]
    public UnityEvent disasterEvent;

    [System.NonSerialized]
    public bool disasterState;
    public bool DisasterState
    {
        get { return disasterState; } //read
        set
        {
            disasterState = value;

            if (disasterEvent != null)
            {
                disasterEvent.Invoke();
                Debug.Log("should work");
            }
        }
    }

    public void SetDisasterStateToFalse()
    {
        DisasterState = false;
    }

    public void SetDisasterStateToTrue()
    {
        DisasterState = true;
    }

    private void OnEnable()
    {
        if (disasterEvent == null)
        {
            disasterEvent = new UnityEvent();
        }
    }
}
