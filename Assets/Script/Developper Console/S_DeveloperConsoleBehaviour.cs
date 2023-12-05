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

    private bool isConsoleActive = false;

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

    private void Update()
    {
        // Check if the console is active
        if (!isConsoleActive)
        {
            return;
        }

        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any collider
            if (Physics.Raycast(ray, out hit))
            {
                // Log the position and name of the clicked object
                Debug.Log(hit.collider.gameObject.name + " at position: " + hit.collider.gameObject.transform.position);
            }
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
            isConsoleActive = false;
        }
        else
        {
            pausedTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            UICanvas.SetActive(true);
            isConsoleActive = true;
            inputField.ActivateInputField();
        }

    }
    public void ProcessCommand(string inputValue)
    {
        DeveloperConsole.ProcessCommand(inputValue);

        inputField.text = string.Empty;
    }
}
