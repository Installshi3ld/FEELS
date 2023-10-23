using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_InputManager : MonoBehaviour
{
    public GameObject LifeExperienceToSpawn;
    public UnityEvent ChangeTimeScale;
    
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
            LifeExperienceList.Add(GameObject.Instantiate(LifeExperienceToSpawn, new Vector3(0,-500, 0), Quaternion.identity));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (GameObject lifeExperience in LifeExperienceList)
            {
                Destroy(lifeExperience);
            }
        }
    }

    
}
