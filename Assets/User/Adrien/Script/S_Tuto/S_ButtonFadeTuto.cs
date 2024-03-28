using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonFade : MonoBehaviour

{
    public Image blackBackground;
    public float fadeDuration = 1.0f;
    public TextMeshProUGUI textToAppear;
    public float delayBeforeTextAppears = 3.0f; // Délai avant l'apparition du texte


    void Start()
    {
        // Désactiver l'image noire au démarrage
        blackBackground.gameObject.SetActive(false);

        StartCoroutine(ShowTextAfterDelay());
    }

    IEnumerator ShowTextAfterDelay()
    {
        // Attendre le délai spécifié
        yield return new WaitForSeconds(delayBeforeTextAppears);

        // Activer le texte à faire apparaître
        textToAppear.gameObject.SetActive(true);
        textToAppear.color = new Color(textToAppear.color.r, textToAppear.color.g, textToAppear.color.b, 0);
        textToAppear.DOFade(1f, fadeDuration);
    }

    public void FadeIn()
    {
        // Activer l'image noire
        blackBackground.gameObject.SetActive(true);

        // Faire progressivement disparaître l'image noire
        blackBackground.DOColor(new Color(0, 0, 0, 0), fadeDuration)
            .OnComplete(() =>
            {
                // Une fois le fondu terminé, désactiver l'image noire
                blackBackground.gameObject.SetActive(false);
            });
    }
}