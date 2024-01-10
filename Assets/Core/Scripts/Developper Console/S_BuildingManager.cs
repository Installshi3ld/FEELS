using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static S_Building;

public class S_BuildingManager : MonoBehaviour
{
    public S_EmotionScriptableObject emotionType;

    [SerializeField]
    private bool isIncreasing;

    [SerializeField]
    public int increaseOrDecreaseAmount;

    S_Building buildingScript;
    private void Start()
    {
        buildingScript = gameObject.GetComponent<S_Building>();
    }
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

    /*
    private void OnBeingPlaced()
    {
        if(feelCost.amount >= price)
        {
            feelCost.RemoveAmount(price);
            ApplyBuildingEffect();
        }
        else
        {
            Debug.Log("not enough money");
        }
    }*/

}
