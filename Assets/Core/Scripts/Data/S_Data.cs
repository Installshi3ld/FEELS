using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class S_Data<T> : ScriptableObject
{
    [SerializeField]
    protected T value;
    public void SetValue(T newValue)
    {
        value = newValue;
    }
    public T GetValue()
    {
        return value;
    }
}