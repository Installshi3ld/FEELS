using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FireLifeExperience : MonoBehaviour
{

    public GameObject smallFire;
    private void Awake()
    {
        
    }

    private void Start()
    {
        this.transform.position = Grid.GetRandomTileInGrid();
        //print(Grid.getIndexbasedOnPosition(Vector3.zero));
        StartCoroutine(FlamePropagation());
    }
    

    IEnumerator FlamePropagation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject.Instantiate(smallFire, Grid.GetRandomTileAroundOtherOne(Grid.getIndexbasedOnPosition(this.transform.position), 5), Quaternion.identity);
        }
        
    }
}
