using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FeelAssignationBuilding : MonoBehaviour
{
    [NonSerialized]
    public int CurrentStoredFeel;
    [NonSerialized]
    public bool StorageFull;
    public int MaxFeel;

    /// <summary>
    /// Return true if successfuly assign feels
    /// </summary>
    /// <param name="feelType"></param>
    /// <returns></returns>
    public bool AssignFeels(S_Currencies feelType)
    {
        if(!StorageFull) { 
            CurrentStoredFeel = MaxFeel;
            feelType.RemoveAmount(MaxFeel);
            StorageFull = true;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Return true if successfuly unassign feels
    /// </summary>
    /// <param name="feelType"></param>
    /// <returns></returns>
    public bool UnassignFeels(S_Currencies feelType)
    {
        if (StorageFull)
        {
            CurrentStoredFeel = 0;
            feelType.AddAmount(MaxFeel);
            StorageFull = false;
            return true;
        }
        return false;
    }
}
