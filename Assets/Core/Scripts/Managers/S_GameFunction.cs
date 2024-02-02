using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class S_GameFunction : MonoBehaviour
{
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static bool isPaused = false;
    public GameObject PauseMenu;
    public S_ScriptableRounds ScriptableRounds;
    public S_MenuData isBuilding;
    public S_MenuData inSkillTreeMenu;

    public void SwitchTimeScalePauseResume()
    {

        //     if (objectSpawned != null)
        //     {
        //          Destroy(objectSpawned);
        //          HidePlanePlacement();
        //          _gridData.ClearPlaneFeedbackBuildingStatement();
        //     }
        if (!isBuilding.value)
        {
            if (Time.timeScale > 0.5f)
            {
                PauseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }

            else
            {
                PauseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
        }
        print("TRes");
            
    }
    //Change round 
    public void ChangeRound()
    {
        ScriptableRounds.SwitchRound();

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
