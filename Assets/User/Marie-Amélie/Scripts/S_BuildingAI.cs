using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BuildingAI : MonoBehaviour
{
    [SerializeField] private GameObject joy;
    [SerializeField] private GameObject anger;
    [SerializeField] private GameObject sadness;
    [SerializeField] private GameObject fear;

    [SerializeField] private int maxPrefabInstances;
    [SerializeField] private int minPrefabInstances;

    [SerializeField] GameObject spawningSpot;

    [SerializeField] private float spawnInterval = 2f; // Spawn interval in seconds

    [SerializeField] S_BuildingList buildingList;

    private List<GameObject> spawnedPrefabs = new List<GameObject>();

    private void Awake()
    {
        S_BuildingList.OnBuildingAdded += AddBuildingFeelsOnMap;
    }

    private void Start()
    {
        StartCoroutine(AddFeelsOnMap(FeelType.Joy, 0));
        StartCoroutine(AddFeelsOnMap(FeelType.Anger, 0));
        StartCoroutine(AddFeelsOnMap(FeelType.Fear, 0));
        StartCoroutine(AddFeelsOnMap(FeelType.Sad, 0));

        // Instantiate initial prefabs on the map
        for (int i = 0; i < minPrefabInstances-4; i++) 
        {
            StartCoroutine(AddFeelsOnMap(GetRandomFeelType(), 1));
        }
    }

    void AddBuildingFeelsOnMap(FeelType type, int numberToSpawn)
    {
        StartCoroutine(AddFeelsOnMap(type, numberToSpawn));
    }
    IEnumerator AddFeelsOnMap(FeelType type, int numberToSpawn)
    {
        GameObject toSpawn = fear;

        switch(type)
        {
            case FeelType.Fear:
                toSpawn = fear;
                break;
            case FeelType.Joy:
                toSpawn = joy;
                break;
            case FeelType.Anger:
                toSpawn = anger;
                break;
            case FeelType.Sad:
                toSpawn = sadness;
                break;

        }

        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnPrefab(toSpawn);
            yield return new WaitForSeconds(spawnInterval);
        }

    }

    private FeelType GetRandomFeelType()
    {
        System.Random random = new System.Random();
        Array feelTypes = Enum.GetValues(typeof(FeelType));
        FeelType randomFeel = (FeelType)feelTypes.GetValue(random.Next(feelTypes.Length));

        return randomFeel;
    }

    private void SpawnPrefab(GameObject toSpawn)
    {
        GameObject spawnedPrefab = Instantiate(toSpawn, spawningSpot.transform.position, toSpawn.transform.rotation);

        spawnedPrefabs.Add(spawnedPrefab);
    }

}