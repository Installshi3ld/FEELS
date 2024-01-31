using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_UISuccess : MonoBehaviour
{
    public TextMeshProUGUI textToFade;

    private void OnEnable()
    {
        UpdateAndFadeText();
    }

    void UpdateAndFadeText()
    {

        // Initial setup: set alpha to zero
        textToFade.color = new Color(textToFade.color.g, textToFade.color.b, textToFade.color.b, 0f);

        // Sequence for fade in and out
        Sequence fadeSequence = DOTween.Sequence();

        // Fade In
        fadeSequence.Append(textToFade.DOFade(1f, 1.0f)); // Fade in text over 1 second

        // Delay
        fadeSequence.AppendInterval(1.0f); // Wait for 1 second

        // Fade Out
        fadeSequence.Append(textToFade.DOFade(0f, 1.0f)); // Fade out text over 1 second

        // Callback when the sequence is complete
        fadeSequence.OnComplete(() =>
        {
            //  Debug.Log("Fade In and Out complete!");
        });

        // Play the sequence
        fadeSequence.Play();
    }
}
