using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_RoundsLeftText : MonoBehaviour
{
    public TextMeshProUGUI tm;
    public S_ScriptableRounds roundsScriptable;

    private void Update()
    {
        RefreshText();
    }

    public void RefreshText()
    {
        tm.text = "Rounds Left :" + roundsScriptable.GetRoundsLeft().ToString();
    }
}
