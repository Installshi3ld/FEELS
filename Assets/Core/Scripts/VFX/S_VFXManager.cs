using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject vfxPrefabJoy;
    [SerializeField] private GameObject vfxPrefabAnger;
    [SerializeField] private GameObject vfxPrefabSad;
    [SerializeField] private GameObject vfxPrefabFear;

    [SerializeField] private List<GameObject> spawningSpotsTsunami;
    public void InstantiateCorrectVFX(FeelType feelType)
    {
        switch (feelType)
        {
            case FeelType.Joy: break;
            case FeelType.Anger: break;
            case FeelType.Sad: InstantiateTsunami(vfxPrefabSad); break;
            case FeelType.Fear: break;
        }
    }

    private void InstantiateTsunami(GameObject vfxPrefab)
    {
        // Generate a random number between 0 (inclusive) and 4 (exclusive)
        int randomNumber = Random.Range(0, 4);

        float yRotation = 0;

        switch (randomNumber)
        {
            case 0: yRotation = 0f ; break;
            case 1: yRotation = 180f ; break;
            case 2: yRotation = -90f ; break;
            case 3: yRotation = 90f ; break;

        }

        Quaternion rotation = Quaternion.Euler(0f, yRotation, 0f);

        GameObject tsunamiInstance = Instantiate(vfxPrefab, spawningSpotsTsunami[randomNumber].transform.position, rotation);
        StartCoroutine(DestroyVFX(tsunamiInstance));

    }

    IEnumerator DestroyVFX(GameObject toDestroy)
    {
        yield return new WaitForSeconds(5f);
        Destroy(toDestroy);
    }

    private void InstantiateThunder()
    {

    }
}
