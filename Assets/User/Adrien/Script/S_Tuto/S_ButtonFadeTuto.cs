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
    public float delayBeforeTextAppears = 3.0f; // D�lai avant l'apparition du texte


    void Start()
    {
        // D�sactiver l'image noire au d�marrage
        blackBackground.gameObject.SetActive(false);

        StartCoroutine(ShowTextAfterDelay());
    }

    IEnumerator ShowTextAfterDelay()
    {
        // Attendre le d�lai sp�cifi�
        yield return new WaitForSeconds(delayBeforeTextAppears);

        // Activer le texte � faire appara�tre
        textToAppear.gameObject.SetActive(true);
        textToAppear.color = new Color(textToAppear.color.r, textToAppear.color.g, textToAppear.color.b, 0);
        textToAppear.DOFade(1f, fadeDuration);
    }

    public void FadeIn()
    {
        // Activer l'image noire
        blackBackground.gameObject.SetActive(true);

        // Faire progressivement dispara�tre l'image noire
        blackBackground.DOColor(new Color(0, 0, 0, 0), fadeDuration)
            .OnComplete(() =>
            {
                // Une fois le fondu termin�, d�sactiver l'image noire
                blackBackground.gameObject.SetActive(false);
            });
    }
}