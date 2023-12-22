using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AIManager : MonoBehaviour
{
    //Ajouté une list currencies, ajouté une fonction pour spawn les IA sur le OnRefreshUI de currencies, créer un fonction qui Despawn les IA.

    public List<GameObject> FeelsTypePrefab;
    public List<S_AI> _aiScript;
    public List<S_Currencies> _currencies;

    List<List<GameObject>> FeelPool = new List<List<GameObject>>();
    int poolSizePerFeels = 50;

    
    void Start()
    {
        /*
        foreach (S_Currencies _currency in _currencies)
        {
            _currency.OnRefreshUi += RefreshFeelAI;
        }*/

        for (int i = 0; i < FeelsTypePrefab.Count;  i++)
        {
            _aiScript.Add(FeelsTypePrefab[i].transform.GetChild(0).GetComponent<S_AI>());

            List<GameObject> tmpList = new List<GameObject>();
            for (int j = 0; j < poolSizePerFeels; j++)
            {
                GameObject tmpFeel = Instantiate(FeelsTypePrefab[i], new Vector3(0, 0, 0), Quaternion.identity);
                tmpList.Add(tmpFeel);
                tmpFeel.SetActive(false);
            }
            FeelPool.Add(tmpList);
        }
        SetFeelActive(true ,FeelType.Fear ,50);
        SetFeelActive(false, FeelType.Fear ,40);

    }

    void RefreshFeelAI()
    {
        int additiveCurrencyAmount = 0;
        foreach (S_Currencies _currency in _currencies)
        {
            additiveCurrencyAmount += _currency.amount;
        }

        foreach (S_Currencies _currency in _currencies)
        {
            if(_currency.amount / additiveCurrencyAmount )
            //GetFeelAmountBaseOnType()
        }
    }

    int GetFeelIndexBaseOnType(FeelType _feelType)
    {
        for(int i = 0; i < FeelsTypePrefab.Count; i++)
        {
            if (_aiScript[i].m_FeelType == _feelType)
                return i;
        }
        return -1;
    }

    int GetFeelAmountBaseOnType(FeelType _feelType)
    {
        int result = 0;
        int index = GetFeelIndexBaseOnType(_feelType);

        for (int i = 0; i < poolSizePerFeels; i++)
        {
            if (FeelPool[index][i].activeSelf == true)
            {
                result++;
            }
        }

        return result;
    }

    GameObject tmp;
    public void SetFeelActive(bool active, FeelType feelType, int amount)
    {
        for (int i = 0;i < amount; i++)
        {
            tmp = GetFeelInPool(GetFeelIndexBaseOnType(feelType), !active);
            if (tmp)
            {
                tmp.transform.position = Vector3.zero;
                tmp.SetActive(active);
            }
        }
    }

    GameObject GetFeelInPool(int intFeelType, bool enabled = false)
    {
        for (int i = 0; i < poolSizePerFeels; i++)
        {
            if (!enabled && !FeelPool[intFeelType][i].activeSelf)
            {
                return FeelPool[intFeelType][i];
            }

            else if (enabled && FeelPool[intFeelType][i].activeSelf)
            {
                return FeelPool[intFeelType][i];
            }
        }
        return null;
    }
    
}
