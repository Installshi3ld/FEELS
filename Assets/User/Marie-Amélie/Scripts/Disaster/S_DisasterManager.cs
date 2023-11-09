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
    private S_DisasterState[] disasterState;
    [SerializeField]
    private S_EmotionScriptableObject[] emotions; // An array of all emotion scriptable objects
    [SerializeField]
    private S_Currencies[] feels; // An array of all feels scriptable objects

    [SerializeField]
    private int differenceToProvokeDisaster;

    [SerializeField]
    private Image disasterImage;

    [SerializeField]
    private float secondsBeforeApplyingDisaster;

    private bool isDisasterWillOccur;


    private void OnEnable()
    {
        for (int i = 0; i < emotions.Length; i++)
        {
            emotions[i].emotionAmountChangeEvent.AddListener(checkForDisaster);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < emotions.Length; i++)
        {
            emotions[i].emotionAmountChangeEvent.RemoveListener(checkForDisaster);
        }
    }

    // Function to find the farthest integer and modify it
    private void ModifyFarthestEmotion()
    {
        if (emotions.Length >= 2)
        {
            S_EmotionScriptableObject farthestEmotion = FindFarthestEmotion();
            farthestEmotion.ChangeEmotionAmout(50);
            Debug.Log("Disaster applied for : " + farthestEmotion.name);
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

    private List<S_EmotionScriptableObject> FindEquilibratedEmotions() //Append in list all emotions except the one that is the most desequilibrated
    {
        List<S_EmotionScriptableObject> equilibratedEmotions = new List<S_EmotionScriptableObject>();
        S_EmotionScriptableObject farthestEm = FindFarthestEmotion();

        foreach (S_EmotionScriptableObject emotion in emotions)
        {
            if(emotion != farthestEm)
            {
                equilibratedEmotions.Add(emotion);
            }
        }

        return equilibratedEmotions;
    }


    private void ReequilibrateEmotionAfterDisaster()
    {
        //(A+B+C)/3 = D (New value of the modified emotion) 
        //On additionne les valeurs des trois autre qu'on divise par trois et c'est la nouvelle valeur de la barre affectée par le désastre

        List<S_EmotionScriptableObject> equilibratedEmotions = FindEquilibratedEmotions();
        int newEmotionValue = 0;

        foreach(S_EmotionScriptableObject emotion in equilibratedEmotions)
        {
            newEmotionValue += emotion.emotionAmount;
        }

        FindFarthestEmotion().ChangeEmotionAmout(newEmotionValue);
    }


    // Update is called once per frame
    private void checkForDisaster() //called for every emotion and just checking if one is desequilibrated and not if its this current and its desequilibrated
    {
        isDisasterWillOccur = false;

        // Iterate through all pairs of emotions
        for (int i = 0; i < emotions.Length; i++)
        {
            for (int j = i + 1; j < emotions.Length; j++)
            {
                if (Mathf.Abs(emotions[i].emotionAmount - emotions[j].emotionAmount) > differenceToProvokeDisaster && !isDisasterWillOccur)
                {
                    isDisasterWillOccur = true;
                    disasterImage.gameObject.SetActive(true);
                    StartCoroutine(checkEquilibriumAndApplyDisaster());
                    Debug.Log("Disaster Will be provoked for " + FindFarthestEmotion().name);
                }

            }
        }
        if (!isDisasterWillOccur)
        {
            disasterImage.gameObject.SetActive(false);
        }
    }

    private IEnumerator checkEquilibriumAndApplyDisaster() //After several seconds we check if the equilibrium stills desequilibrated and apply disaster if needed
    {
        yield return new WaitForSeconds(secondsBeforeApplyingDisaster);

        checkForDisaster();

        if (isDisasterWillOccur)
        {
            ProvokeDisaster();
        }
    }

    private void ProvokeDisaster()
    {
        //ModifyFarthestEmotion();
        ReequilibrateEmotionAfterDisaster(); //Reequilibrate emotion after disaster with the new formula
        //Consume Feel of corresponding type
        Debug.Log("provoked");
    }
}





