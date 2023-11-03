using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class S_DisasterManager : MonoBehaviour
{
    [SerializeField]
    private S_EmotionScriptableObject[] emotions; // An array of all emotion scriptable objects

    [SerializeField]
    private int differenceToProvokeDisaster;


    // Update is called once per frame
    void Update()
    {
        // Iterate through all pairs of emotions
        for (int i = 0; i < emotions.Length; i++)
        {
            for (int j = i + 1; j < emotions.Length; j++)
            {
                if (Mathf.Abs(emotions[i].emotionAmount - emotions[j].emotionAmount) > differenceToProvokeDisaster)
                {
                    Debug.Log("There is more than a 50-point difference between " + emotions[i].name + " and " + emotions[j].name);
                }
            }
        }
    }
}





