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
        // Assurez-vous que le texte est invisible au d�but
        textDisasterHere.alpha = 0f;

      
    }

    public void DisasterIsHere()
    {
        // Appelez la fonction BlinkText apr�s un d�lai de 2 secondes (vous pouvez ajuster ce d�lai selon vos besoins)
        Invoke("StartBlinking", 2f);
    }

    void StartBlinking()
    {
        // Utilisez DOTween pour faire appara�tre puis dispara�tre le texte sur une dur�e totale de 4 secondes
        textDisasterHere.DOFade(1f, 0.75f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            // Appel� une fois que l'animation est termin�e
            HideText();
        });
    }

    void HideText()
    {
        // Utilisez DOTween pour faire dispara�tre le texte sur une dur�e de 1 seconde
        textDisasterHere.DOFade(0f, 1f).OnComplete(() =>
        {
            // Appel� une fois que l'animation est termin�e
            Debug.Log("Text hidden");
        });
    }
}


