using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_InputManager : MonoBehaviour
{
    public UnityEvent ChangeTimeScale;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeTimeScale.Invoke();
        }
    }
}
