using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class S_DisasterIsHere : MonoBehaviour
{
    public TextMeshProUGUI textDisasterHere;

    void Start()
    {
        textDisasterHere.alpha = 0f;
    }

    public void DisasterIsHere()
    {
        Invoke("StartBlinking", 2f);
    }

    void StartBlinking()
    {
        textDisasterHere.DOFade(1f, 0.75f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            HideText();
        });
    }

    void HideText()
    {
        textDisasterHere.DOFade(0f, 1f).OnComplete(() =>
        {
           // Debug.Log("Text hidden");
        });
    }
}


