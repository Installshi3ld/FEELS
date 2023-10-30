using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BuildingManager : MonoBehaviour
{
    [SerializeField]
    private S_EmotionScriptableObject emotionType;

    [SerializeField]
    private bool isIncreasing;

    [SerializeField]
    private int increaseOrDecreaseAmount;

    [SerializeField]
    private S_Currencies feelType;

    [SerializeField]
    private int price;

    private void ApplyBuildingEffect()
    {
        if (isIncreasing)
        {
            emotionType.IncreaseEmotion(increaseOrDecreaseAmount);
        }
        else
        {
            emotionType.DecreaseEmotion(increaseOrDecreaseAmount);
        }
    }
    private void OnBeingPlaced()
    {
        if(feelType.amount >= price)
        {
            feelType.amount -= price;
            ApplyBuildingEffect();
        }
        else
        {
            Debug.Log("not enough money");
        }
    }

}
