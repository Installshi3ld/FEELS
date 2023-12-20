using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AIManager : MonoBehaviour
{
    //Ajouté une list currencies, ajouté une fonction pour spawn les IA sur le OnRefreshUI de currencies, créer un fonction qui Despawn les IA.

    public List<GameObject> FeelsTypePrefab;
    public List<S_AI> _aiScript;

    List<List<GameObject>> FeelPool = new List<List<GameObject>>();
    int poolSizePerFeels = 300;

    
    void Start()
    {
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
        ShowFeel(FeelType.Fear ,50);

    }

    int GetFeelIndexBaseOnType(FeelType _feelType)
    {
        for(int i = 0; i < FeelsTypePrefab.Count; i++)
        {
            if (_aiScript[i].m_FeelType == _feelType)
                return i;
        }
        print("ferfr");
        return -1;
    }

    GameObject tmp;
    public void ShowFeel(FeelType feelType, int amount)
    {
        for (int i = 0;i < amount; i++)
        {
            tmp = GetFeelInPool(GetFeelIndexBaseOnType(feelType));
            if (tmp)
            {
                tmp.transform.position = Vector3.zero;
                tmp.SetActive(true);
            }
        }
    }


    GameObject GetFeelInPool(int intFeelType)
    {
        for (int i = 0; i < poolSizePerFeels; i++)
        {
            if (!FeelPool[intFeelType][i].activeSelf)
            {
                return FeelPool[intFeelType][i];
            }
        }
        return null;
    }
    
}
