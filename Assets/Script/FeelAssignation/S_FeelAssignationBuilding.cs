using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FeelAssignationBuilding : MonoBehaviour
{
    [NonSerialized]
    public int CurrentStoredFeel;
    [NonSerialized]
    public bool isProducing, isBoosted = false;
    public int MaxFeel;

    [Header("Normal")]
    public float productionAmount = 2.0f;
    public float delayBetweenEachProduction = 2.0f;

    [Header("Boosted")]
    public float productionAmountBoosted = 4.0f;
    public float delayBetweenEachProductionBoosted = 2.0f;

    float currentProduction, currenteDelayBetweenEachProduction;

    [NonSerialized]
    public float delayBetweenEachProductionForUI = 0;
    [NonSerialized]
    public float productionAmountForUI = 0;

    S_Currencies feelProductionType;

    bool bProductionFeels;
    private void Awake()
    {
        currentProduction = productionAmount;
        currenteDelayBetweenEachProduction = delayBetweenEachProduction;
    }
    private void Start()
    {
        if (gameObject.TryGetComponent(out S_Building _building))
        {
            var prices = _building.GetCosts();

            if (prices[0].feelTypeCurrency && prices[0].feelTypeCurrency.feelType == S_Currencies.FeelType.Anger)
                BoostBuilding();
        }
    }
    /// <summary>
    /// Return true if successfuly assign feels
    /// </summary>
    /// <param name="feelType"></param>
    /// <returns></returns>
    public bool AssignFeels(S_Currencies feelType)
    {
        if(!isProducing) {
            delayBetweenEachProductionForUI = delayBetweenEachProduction;
            productionAmountForUI = productionAmount;

            feelProductionType = feelType;
            CurrentStoredFeel = MaxFeel;
            feelType.RemoveAmount(MaxFeel);
            isProducing = true;
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
        if (isProducing)
        {
            delayBetweenEachProductionForUI = 0;
            productionAmountForUI = 0;

            CurrentStoredFeel = 0;
            feelType.AddAmount(MaxFeel);
            isProducing = false;
            StopAllCoroutines();
            return true;
        }
        return false;
    }

    IEnumerator FeelProduction()
    {
        while(true)
        {
            yield return new WaitForSeconds(currenteDelayBetweenEachProduction);
            feelProductionType.AddAmount(productionAmount);
        }
    }

    public void BoostBuilding()
    {
        S_FeelAssignationManager.SpawnVFXBoost(gameObject.transform.GetChild(0));
        isBoosted = true;
        currentProduction = productionAmountBoosted;
        currenteDelayBetweenEachProduction = delayBetweenEachProductionBoosted;
    }

    public void UnBoostBuilding()
    {
        isBoosted = false;
        currentProduction = productionAmount;
        currenteDelayBetweenEachProduction = delayBetweenEachProduction;
    }
}
