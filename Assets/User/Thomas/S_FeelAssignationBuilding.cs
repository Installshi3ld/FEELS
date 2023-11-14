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

    public float delayBetweenEachProduction = 2.0f;
    public float productionAmount = 2.0f;

    [NonSerialized]
    public float delayBetweenEachProductionForUI = 0;
    [NonSerialized]
    public float productionAmountForUI = 0;

    S_Currencies feelProductionType;
    bool bProductionFeels;
    /// <summary>
    /// Return true if successfuly assign feels
    /// </summary>
    /// <param name="feelType"></param>
    /// <returns></returns>
    public bool AssignFeels(S_Currencies feelType)
    {
        if(!StorageFull) {
            delayBetweenEachProductionForUI = delayBetweenEachProduction;
            productionAmountForUI = productionAmount;

            feelProductionType = feelType;
            CurrentStoredFeel = MaxFeel;
            feelType.RemoveAmount(MaxFeel);
            StorageFull = true;
            StartCoroutine(FeelProduction());
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
            delayBetweenEachProductionForUI = 0;
            productionAmountForUI = 0;

            CurrentStoredFeel = 0;
            feelType.AddAmount(MaxFeel);
            StorageFull = false;
            StopCoroutine(FeelProduction());
            return true;
        }
        return false;
    }

    IEnumerator FeelProduction()
    {
        while(true)
        {
            yield return new WaitForSeconds(delayBetweenEachProduction);
            feelProductionType.AddAmount(productionAmount);
        }
    }
}
