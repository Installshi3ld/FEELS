using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class S_DeveloperConsoleBehavior : MonoBehaviour
{
    [SerializeField] private string prefix = string.Empty;
    [SerializeField] private S_ConsoleCommand[] commands = new S_ConsoleCommand[0];

    [Header("UI")]
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private TMP_InputField inputField = null;

    private float pausedTimeScale;

    private S_DeveloperConsole developerConsole;
    private static S_DeveloperConsoleBehavior instance;

    private S_DeveloperConsole DeveloperConsole
    {
        get
        {
            if(developerConsole != null) { return developerConsole; }
            return developerConsole = new S_DeveloperConsole(prefix, commands);

        }
    }

    private void Awake()
    {
        if(instance!=null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Toggle(CallbackContext context)
    {
        if(!context.action.triggered)
        {
            return;
        }

        if (UICanvas.activeSelf)
        {
            Time.timeScale = pausedTimeScale;
            UICanvas.SetActive(false);
        }
        else
        {
            pausedTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            UICanvas.SetActive(true);
            inputField.ActivateInputField();
        }

    }

    public void ProcessCommand(string inputValue)
    {
        DeveloperConsole.ProcessCommand(inputValue);

        inputField.text = string.Empty;
    }
}
