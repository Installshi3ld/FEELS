using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TimeManager : MonoBehaviour
{
    private void PauseGame() //Apply to everything except audio and maybe inputs //If you need to prevent certain objects from being paused consider using unscaled fixed time 
    {
        Time.timeScale = 0f;
    }

    private void StopPauseGame()
    {
        Time.timeScale = 1f;
    }
}
