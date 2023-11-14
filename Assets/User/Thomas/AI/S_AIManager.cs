using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AIManager : MonoBehaviour
{
    public List<GameObject> FeelsTypePrefab;

    List<List<GameObject>> FeelPool = new List<List<GameObject>>();
    int poolSizePerFeels = 500;

    public enum FeelType
    {
        Joy,
        Anger,
        Sad,
        Fear
    }

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < 4;  i++)
        {
            List<GameObject> tmpList = new List<GameObject>();
            for (int j = 0; j < poolSizePerFeels; j++)
            {
                GameObject tmpFeel = Instantiate(FeelsTypePrefab[i], new Vector3(0, -15, 0), Quaternion.identity);
                tmpList.Add(tmpFeel);
                tmpFeel.SetActive(false);
            }
            FeelPool.Add(tmpList);
        }

        ShowFeel(FeelType.Anger ,1);

    }

    GameObject tmp;
    public void ShowFeel(FeelType feelType, int amount)
    {
        for (int i = 0;i < amount; i++)
        {
            tmp = GetFeelInPool(((int)feelType));
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
