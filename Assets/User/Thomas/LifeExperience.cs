using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeExperience : MonoBehaviour
{
    public GameObject LifeExperience3DModel;
    public Collider Trauma;
    public Button PanelButton;
    public GameObject TraumaFlam;
    public List<GameObject> TraumaList;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;


    private void Awake()
    {
        this.transform.position = Grid.GetRandomTileInGrid();
    }
    public void Start()
    {
        PanelButton.enabled = false;
    }
    public void ActivateGravity()
    {
        LifeExperience3DModel.GetComponent<Rigidbody>().useGravity = true;
    }

    void OnTriggerEnter(Collider collision)

    {
        if (collision.gameObject.tag == "trauma")
        {
            Debug.Log("touche");
            PanelButton.enabled = true;
            stopSpawning = true;
            StartCoroutine(SpawnTrauma());
        }


    }
    public IEnumerator SpawnTrauma()
    {
        if (stopSpawning == true ) 


            {

                Vector3 randomSpawnPosition = new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));

                GameObject TraumaInstance = Instantiate(TraumaFlam, randomSpawnPosition, Quaternion.identity);

                TraumaList.Add(TraumaInstance);

            yield return new WaitForSeconds(spawnDelay);

            StartCoroutine(SpawnTrauma());

        }

    }

    public void DestroyFlam()
    {

        stopSpawning = false;

        foreach (GameObject TraumaFlam in TraumaList)

        {
            Destroy(TraumaFlam);
        }

    }



}
