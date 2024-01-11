using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;


public class S_UIConsoleText : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> logTextFields = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<TextMeshProUGUI> logHourTextFields = new List<TextMeshProUGUI>();


    private int currentIndex;
    private int previousIndex;

    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
        currentIndex = 0;
        previousIndex = 4; //prevent the first text from being white
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }
    void LogCallback(string logString, string stackTrace, LogType type)
    {
        logHourTextFields[currentIndex].text = System.DateTime.Now.ToString();
        logHourTextFields[currentIndex].color = new Color(0.153f, 0.161f, 0.42f); 

        logTextFields[currentIndex].text = logString;
        logTextFields[currentIndex].color = new Color(0.153f, 0.161f, 0.42f);

        logTextFields[previousIndex].color = new Color(0.153f, 0.161f, 0.42f, 0.741f);
        logHourTextFields[previousIndex].color = new Color(0.153f, 0.161f, 0.42f, 0.741f);

        previousIndex = currentIndex;
        currentIndex++;

        if(currentIndex == logTextFields.Count)
        {
            currentIndex = 0;
        }
    }
}