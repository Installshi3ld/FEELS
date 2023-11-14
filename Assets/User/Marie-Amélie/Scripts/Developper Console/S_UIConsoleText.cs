using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class S_UIConsoleText : MonoBehaviour
{
    /*
    public TextMeshProUGUI logTextPrefab; // Reference to TextMeshProUGUI prefab
    public Transform logPanel; // Reference to the parent panel where you want to instantiate the logs
    public float logSpacing = 5f; // Vertical spacing between log messages

    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }

    void LogCallback(string logString, string stackTrace, LogType type)
    {
        // Create a new TextMeshProUGUI element dynamically
        TextMeshProUGUI newLogText = Instantiate(logTextPrefab, logPanel);

        // Set the log message to the new text element
        newLogText.text = logString;

        // Adjust the position of the new log element based on its index and spacing
        RectTransform rt = newLogText.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -logPanel.childCount * (rt.sizeDelta.y + logSpacing));
    }*/

    public TextMeshProUGUI logTextPrefab; // Reference to TextMeshProUGUI prefab
    public Transform logPanel; // Reference to the parent panel where you want to instantiate the logs
    public float logSpacing = 5f; // Vertical spacing between log messages

    private HashSet<string> loggedMessages = new HashSet<string>();

    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }
    void LogCallback(string logString, string stackTrace, LogType type)
    {
        // Check if the message has already been logged
        if (loggedMessages.Contains(logString))
        {
            return;
        }

        // Add the message to the set of logged messages
        loggedMessages.Add(logString);

        // Create a new TextMeshProUGUI element dynamically
        TextMeshProUGUI newLogText = Instantiate(logTextPrefab, logPanel);

        // Set the log message to the new text element
        newLogText.text = logString;

        // Adjust the position of the new log element based on its index and spacing
        RectTransform rt = newLogText.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -logPanel.childCount * (rt.sizeDelta.y + logSpacing));
    }
}

/*
public TextMeshProUGUI logText;
void OnEnable()
{
    Application.logMessageReceived += LogCallback;
}

void OnDisable()
{
    Application.logMessageReceived -= LogCallback;
}

void LogCallback(string logString, string stackTrace, LogType type)
{
    logText.text = logString;
    //Or Append the log to the old one
    //logText.text += logString + "\r\n";
}
*/