using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class S_DisasterManager : MonoBehaviour
{
    [SerializeField]
    private S_EmotionScriptableObject[] emotions; // An array of all emotion scriptable objects
    [SerializeField]
    private S_Currencies[] feels; // An array of all feels scriptable objects

    [SerializeField]
    private int differenceToProvokeDisaster;

    [SerializeField]
    private Image disasterImage;

    public bool isDisasterOccurring;


    // Function to find the farthest integer and modify it
    public void ModifyFarthestEmotion()
    {
        if (emotions.Length >= 2)
        {
            S_EmotionScriptableObject farthestEmotion = FindFarthestEmotion();
            farthestEmotion.ChangeEmotionAmout(50);
            Debug.Log("Farthest Integer: " + farthestEmotion.name);
        }

    }

    // Function to find the integer that is farthest from the others
    private S_EmotionScriptableObject FindFarthestEmotion()
    {
        int maxDistance = 0;
        S_EmotionScriptableObject farthestEmotion = emotions[0];

        for (int i = 0; i < emotions.Length; i++)
        {
            S_EmotionScriptableObject currentEmotion = emotions[i];
            int distanceSum = 0;

            for (int j = 0; j < emotions.Length; j++)
            {
                if (i != j)
                {
                    distanceSum += Mathf.Abs(currentEmotion.emotionAmount - emotions[j].emotionAmount);
                }
            }

            if (distanceSum > maxDistance)
            {
                maxDistance = distanceSum;
                farthestEmotion = currentEmotion;
            }
        }

        return farthestEmotion;
    }

    
    // Update is called once per frame
    void Update()
    {
        isDisasterOccurring = false;

        // Iterate through all pairs of emotions
        for (int i = 0; i < emotions.Length; i++)
        {
            Debug.Log(emotions[i].name + " amount = " + emotions[i].emotionAmount);
            for (int j = i + 1; j < emotions.Length; j++)
            {
                if (Mathf.Abs(emotions[i].emotionAmount - emotions[j].emotionAmount) > differenceToProvokeDisaster)
                {
                    isDisasterOccurring = true;
                }

            }
        }
        if (isDisasterOccurring)
        {
            Debug.Log("Disaster");
            ProvokeDisaster();
        }
        else
        {
            disasterImage.gameObject.SetActive(false);
        }
    }

    private void ProvokeDisaster()
    {
        disasterImage.gameObject.SetActive(true);
        ModifyFarthestEmotion();
    }
}





