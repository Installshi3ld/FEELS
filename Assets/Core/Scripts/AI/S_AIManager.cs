using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class S_AIManager : MonoBehaviour
{
    [SerializeField] private FeelsMatchSprite joy;
    [SerializeField] private FeelsMatchSprite anger;
    [SerializeField] private FeelsMatchSprite sadness;
    [SerializeField] private FeelsMatchSprite fear;

    [SerializeField] private float prefabRatio = 0.2f; // Adjusted ratio
    [SerializeField] private int maxPrefabInstances = 300;
    [SerializeField] private int minPrefabInstances = 300;

    private int currentMaxPrefab = 0;

    [SerializeField] private float spawnInterval = 2f; // Spawn interval in seconds

    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    private Dictionary<FeelsMatchSprite, int> PrefabsPerFeelType = new Dictionary<FeelsMatchSprite, int>();

    private void Start()
    {
        // Instantiate initial prefabs on the map

        currentMaxPrefab = minPrefabInstances;

        PrefabsPerFeelType.Add(joy, 0);
        PrefabsPerFeelType.Add(fear, 0);
        PrefabsPerFeelType.Add(anger, 0);
        PrefabsPerFeelType.Add(sadness, 0);

        StartCoroutine(MonitorAgentCount());
        StartCoroutine(AutoIncrementMaxPrefab());
    }

    private IEnumerator MonitorAgentCount()
    {
        while (true)
        {
            int totalFeel = joy.feelType.amount + anger.feelType.amount + sadness.feelType.amount + fear.feelType.amount;

            MonitorSpecificFeel(joy, totalFeel);
            MonitorSpecificFeel(fear, totalFeel);
            MonitorSpecificFeel(anger, totalFeel);
            MonitorSpecificFeel(sadness, totalFeel);

            yield return new WaitForSeconds(spawnInterval);
        }

    }

private void MonitorSpecificFeel(FeelsMatchSprite targetFeel, int totalFeel)
    {
        float feelRatio = (float)targetFeel.feelType.amount / (float)totalFeel;

        int numberOfDesiredInstances = Mathf.FloorToInt(feelRatio * currentMaxPrefab);

        int currentNumberOfInstance = PrefabsPerFeelType[targetFeel];

        Debug.Log("current " + currentNumberOfInstance + " desired " + numberOfDesiredInstances);
        if(currentNumberOfInstance >= numberOfDesiredInstances)
        {
            return;
        }

        int instancesToSpawn = (numberOfDesiredInstances - currentNumberOfInstance) / 2 + 1;

        SpawnPrefab(targetFeel, instancesToSpawn);

        PrefabsPerFeelType[targetFeel] += instancesToSpawn;
    }

    IEnumerator AutoIncrementMaxPrefab()
    {
        while(currentMaxPrefab < maxPrefabInstances)
        {
            yield return new WaitForSeconds(2f);
            currentMaxPrefab += 2;
        }
    }

    private void SpawnPrefab(FeelsMatchSprite toSpawn, int instancesToSpawn)
    {
        for (int i = 0; i < instancesToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPositionOnNavMesh();
            GameObject spawnedPrefab = Instantiate(toSpawn.feelSprite, randomPosition, toSpawn.feelSprite.transform.rotation);

            var truc = spawnedPrefab.GetComponent<S_BehaviorAI>();
            Debug.Log(truc);

            //spawnedPrefab.GetComponent<S_BehaviorAI>().Toto(this, toSpawn);
            spawnedPrefabs.Add(spawnedPrefab);
        }
    }

    private Vector3 GetRandomPositionOnNavMesh()
    {
        float xCoord = UnityEngine.Random.Range(this.gameObject.transform.position.x - 30, this.gameObject.transform.position.x + 30);
        float zCoord = UnityEngine.Random.Range(this.gameObject.transform.position.z - 30, this.gameObject.transform.position.z + 30);

        return new Vector3(xCoord, 0, zCoord);
    }

    public void IAmDead(FeelsMatchSprite sprite)
    {
        PrefabsPerFeelType[sprite] -= 1;
    }
}

[Serializable]
public struct FeelsMatchSprite
{
    public S_Currencies feelType;
    public GameObject feelSprite;
}