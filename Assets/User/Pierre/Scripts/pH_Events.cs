using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pH_Events : MonoBehaviour
{
    public pH_Currencies CurrenciesReference;

    public int eventDelayAmount;

    int tierCounter;
    public int currentTier;

    public EventTier[] eventTiers;

    [System.Serializable]
    public class Event
    {
        public EventConstraintType constraintType;
        public int constraintIndex;
        public int constraintValue;

        public bool MeetsConstraint()
        {
            switch(constraintType)
            {
                case EventConstraintType.totalPop:

                    return pH_Currencies.Instance.TotalPop() >= constraintValue;

  
                case EventConstraintType.currencyPop:

                    return pH_Currencies.Instance.currencies[constraintIndex].amount >= constraintValue;

                case EventConstraintType.specificBuilding:

                    return pH_Currencies.Instance.currencies[constraintIndex].amount >= constraintValue;

                case EventConstraintType.currencyBuildingAmount:

                    return pH_Currencies.Instance.currencies[constraintIndex].amount >= constraintValue;

            }

            return true;
        }

    }

    [System.Serializable]
    public class EventTier
    {
        public int eventAmount;
        public Event[] events;
    }

    private void Start()
    {
        timer = delay;
    }

    float delay;
    float timer;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            timer += delay;
            PlayEvent();
        }
    }

    void PlayEvent()
    {



        tierCounter++;
        if(tierCounter >= eventTiers[currentTier].eventAmount && tierCounter < eventTiers.Length-1)
        {
            tierCounter = 0;
            currentTier++;
        }
    }
}

public enum EventConstraintType
{
    totalPop,
    currencyPop,
    currencyBuildingAmount,
    specificBuilding
}
