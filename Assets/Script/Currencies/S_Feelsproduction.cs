using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Feelsproduction : MonoBehaviour
{
    public S_Currencies AngryCurrency, FearCurrency, JoyCurrency, SadCurrency;

    public SO_MoodPercent AngryPercent, FearPercent, JoyPercent, SadPercent;

    public float DelayBetweenProduction = 1;
    public float ProductionAmount = 4;

    void Start()
    {
        StartCoroutine(FeelsProduction());
    }

    IEnumerator FeelsProduction()
    {
        while (true)
        {
            yield return new WaitForSeconds(DelayBetweenProduction);
            AngryCurrency.AddAmount(ProductionAmount * (AngryPercent.percent / 100));
            FearCurrency.AddAmount(ProductionAmount * (FearPercent.percent / 100));
            JoyCurrency.AddAmount(ProductionAmount * (JoyPercent.percent / 100));
            SadCurrency.AddAmount(ProductionAmount * (SadPercent.percent / 100));
        }
        
    }
}
