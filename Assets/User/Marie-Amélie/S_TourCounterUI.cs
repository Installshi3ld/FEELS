using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class S_TourCounterUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterTextFied;
    [SerializeField] private S_ScriptableRounds counter;

    private void Update()
    {
        counterTextFied.text = "Round " + counter.GetNumberOfRounds().ToString();
    }
}
