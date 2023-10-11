using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Gauge : MonoBehaviour
{
    public int emotionIndex;

    [SerializeField]
    private Slider slider;

    public S_GameInfosScriptableObject playerInfos;


    /*public void SetMaxSliderValue(int maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = 0; //to put the emotions and the gauge at 0 when the game starts.
    }*/

    private void OnEnable()
    {
        playerInfos.emotionFeelsChangeEvent.AddListener(SetSliderValue);
    }

    private void OnDisable()
    {
        playerInfos.emotionFeelsChangeEvent.RemoveListener(SetSliderValue);
    }

    public void SetSliderValue()
    {
        Debug.Log("working");
        slider.value = playerInfos.emotions[emotionIndex];
    }
}
