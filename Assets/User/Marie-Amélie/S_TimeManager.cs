using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class S_TimeManager : MonoBehaviour
{
    private float timer;



    void Start()
    {
        timer = 0;
    }

    void Update()
    {

        timer += Time.deltaTime;
    }
    private void PauseGame() //Apply to everything except audio and maybe inputs //If you need to prevent certain objects from being paused consider using unscaled fixed time 
    {
        Time.timeScale = 0f;
    }

    private void StopPauseGame()
    {
        Time.timeScale = 1f;
    }
}
