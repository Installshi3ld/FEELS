using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class S_FadeInOnEnableUI : MonoBehaviour
{
    public float fadeDuration = 5.0f;
    public TextMeshProUGUI textToAppear;
    public GameObject uiToAppear;
    public float delayBeforeTextAppears = 6.0f; // Délai avant l'apparition du texte

    void OnEnable()
    {
        StartCoroutine(ShowTextAfterDelay());
        FadeIn();
    }

    IEnumerator ShowTextAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeTextAppears);

        textToAppear.gameObject.SetActive(true);
        textToAppear.color = new Color(textToAppear.color.r, textToAppear.color.g, textToAppear.color.b, 0);
        textToAppear.DOFade(1f, fadeDuration);
    }

    public void FadeIn()
    {
        // Obtenez ou ajoutez un composant CanvasGroup pour contrôler l'alpha du GameObject
        CanvasGroup canvasGroup = uiToAppear.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = uiToAppear.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f; // Assurez-vous que le GameObject est initialement transparent

        // Faites un fondu pour faire apparaître progressivement le GameObject
        canvasGroup.DOFade(1f, fadeDuration);
    }
}