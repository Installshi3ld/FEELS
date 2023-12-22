r using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameFunction : MonoBehaviour
{
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static bool isPaused = false;
    public GameObject PauseMenu;
    public void SwitchTimeScalePauseResume()
    {
        if (Time.timeScale > 0.5)
        {
            PauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0.000f;
        }

        else
        {
            PauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
            
    }

    public void PauseTimeResume()
    {
        if (Time.timeScale <=0.001f)
        {
            PauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode(); // Quitte le mode lecture
        #endif
                Application.Quit();
    }

}
