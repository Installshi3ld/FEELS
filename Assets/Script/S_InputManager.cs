using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_InputManager : MonoBehaviour
{
    public GameObject LifeExperienceToSpawn;
    public UnityEvent ChangeTimeScale;

    public UnityEvent SpawnFireLifeExperience;
    public UnityEvent DestroyFireLifeExperience;
    
    List<GameObject> LifeExperienceList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeTimeScale.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SpawnFireLifeExperience.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            DestroyFireLifeExperience.Invoke();
        }
    }

    
}
