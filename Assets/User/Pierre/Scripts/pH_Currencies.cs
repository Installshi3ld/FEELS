using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pH_Currencies : MonoBehaviour
{

    public static pH_Currencies Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Currency[] currencies;
    public TierValues[] tierValues;

    [System.Serializable]
    public class Currency
    {
        public string currencyName;
        public Color color;
        public int amount;
        public int production;

        public bool CanCoverCost(int _cost)
        {
            return amount >= _cost;
        }

        public void Spend(int _amount)
        {
            amount -= _amount;
        }

        public void Increase()
        {
            amount += production;
        }
    }

    [System.Serializable]
    public class TierValues
    {
        public int cost;
        public int production;
    }


    public void TryIncreaseProduction(string _currencyName, int _buildingTier)
    {
        for (int i = 0; i < currencies.Length; i++)
        {
            if (currencies[i].currencyName == _currencyName)
            {
                continue;
            }

            if ((!currencies[i].CanCoverCost(tierValues[_buildingTier].cost)))
            {
                return;
            }

        }

        for (int i = 0; i < currencies.Length; i++)
        {
            if (currencies[i].currencyName == _currencyName)
            {
                continue;
            }

            currencies[i].Spend(tierValues[_buildingTier].cost);

        }

        currencies[CurrencyIndexByName(_currencyName)].production += tierValues[_buildingTier].production;
    }

    public int CurrencyIndexByName(string _currencyName)
    {
        for (int i = 0; i < currencies.Length; i++)
        {
            if (currencies[i].currencyName == _currencyName)
            {
                return i;
            }
        }

        Debug.LogError("No currency by name " + _currencyName);
        return -1;
    }


    public int TotalPop()
    {
        int total = 0;
        for (int i = 0; i < currencies.Length; i++)
        {
            total += currencies[i].amount;
        }

        return total;
    }

    // in seconds
    public int productionDelay;

    void Start()
    {
        StartCoroutine(productionTimer());
    }

    void Update()
    {
        
    }

    private IEnumerator productionTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(productionDelay);

            for (int i = 0; i < currencies.Length; i++)
            {
                currencies[i].Increase();
            }

            Debug.Log("seconds");
        }
    }
}
