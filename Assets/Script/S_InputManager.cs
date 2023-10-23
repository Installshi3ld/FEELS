using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_InputManager : MonoBehaviour
{
    public GameObject LifeExperience;
    public UnityEvent ChangeTimeScale;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeTimeScale.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameObject.Instantiate(LifeExperience, new Vector3(0,-500, 0), Quaternion.identity) ;
        }
    }

    
}
