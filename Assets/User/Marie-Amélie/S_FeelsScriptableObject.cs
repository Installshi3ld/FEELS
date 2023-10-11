using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Scriptable Object", menuName = "JOY")]
public class S_JoyScriptableObject : ScriptableObject
{
    [System.NonSerialized]
    public int joyFeels;

    [System.NonSerialized]
    public UnityEvent joyChangeEvent;

    public int JoyFeels
    {
        get { return joyFeels; } //read
        set
        {
            if (value > 100)
            {
                joyFeels = 100;
            }
            else
            {
                joyFeels = value;
            }
            
            joyChangeEvent.Invoke();
        }

    }
    private void OnEnable()
    {
        joyFeels = 0;

        if (joyChangeEvent == null)
        {
            joyChangeEvent = new UnityEvent();
        }
    }

    public void IncreaseJoy(int amount)
    {
        joyFeels += amount;
        //joyChangeEvent.Invoke();
    }

    public void DecreaseJoy(int amount)
    {
        joyFeels -= amount;
        //joyChangeEvent.Invoke();
    }
}
