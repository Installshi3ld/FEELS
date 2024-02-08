using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class S_RoundsLeftText : MonoBehaviour
{
    public TextMeshProUGUI tm;
    public S_ScriptableRounds roundsScriptable;

    private void Update()
    {
        RefreshText();
    }
   // bool tmp = true;
   
    public void RefreshText()
    {
        tm.text = "Rounds Left: " + roundsScriptable.GetRoundsLeft().ToString();
     //  
     //  if (tmp)
     //  {
     //      Transform tmTransform = tm.transform;
     //      tmTransform.DOShakeScale(1f, 0.3f, 1, 10f, false);
     //      tmp = false;
     //  }
     //  else
     //  {
     //      Transform tmTransform = tm.transform;
     //      tmTransform.DOShakeScale(0f, 0f, 0, 0f, false);
     //  }
    }
}
