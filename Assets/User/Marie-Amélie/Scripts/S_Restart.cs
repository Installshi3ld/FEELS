using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Restart : MonoBehaviour
{
    public void RestartGame()
    {
        if (SceneManager.GetActiveScene() != null && gameObject != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        }
    }
}
