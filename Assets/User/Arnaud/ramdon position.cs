using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ramdonposition : MonoBehaviour

{
    public GameObject Trauma;
    public List<GameObject> TraumaList;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    public void Start()
    {
        InvokeRepeating("SpawnTrauma", spawnTime, spawnDelay);
    }

    public void SpawnTrauma()
    {
    //if(Input.GetKeyDown(KeyCode.Space))
        {

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));

            GameObject TraumaInstance = Instantiate(Trauma, randomSpawnPosition, Quaternion.identity);

            TraumaList.Add(TraumaInstance);

            if(stopSpawning ) { CancelInvoke("SpawnTrauma"); }

        }
    }

    public void DestroyFlam()
    {

        CancelInvoke("SpawnTrauma");

        foreach(GameObject Trauma in TraumaList) 
        
        {
            Trauma.SetActive(false);
            
        };

    }

}
