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
        // Assurez-vous que le texte est invisible au début
        textDisasterHere.alpha = 0f;

      
    }

    public void DisasterIsHere()
    {
        // Appelez la fonction BlinkText après un délai de 2 secondes (vous pouvez ajuster ce délai selon vos besoins)
        Invoke("StartBlinking", 2f);
    }

    void StartBlinking()
    {
        // Utilisez DOTween pour faire apparaître puis disparaître le texte sur une durée totale de 4 secondes
        textDisasterHere.DOFade(1f, 0.75f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            // Appelé une fois que l'animation est terminée
            HideText();
        });
    }

    void HideText()
    {
        // Utilisez DOTween pour faire disparaître le texte sur une durée de 1 seconde
        textDisasterHere.DOFade(0f, 1f).OnComplete(() =>
        {
            // Appelé une fois que l'animation est terminée
            Debug.Log("Text hidden");
        });
    }
}


