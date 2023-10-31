using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static S_Building;

public class S_BuildingManager : MonoBehaviour
{
    [SerializeField]
    private S_EmotionScriptableObject emotionType;

    [SerializeField]
    private bool isIncreasing;

    [SerializeField]
    private int increaseOrDecreaseAmount;

    [SerializeField]
    public S_Currencies feelCost;

    [SerializeField]
    private int price;

    S_Building buildingScript;
    private void Start()
    {
        buildingScript = gameObject.GetComponent<S_Building>();
        buildingScript.changingEquilibriumValue += ApplyBuildingEffect;
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
    }

}
