using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class S_UIFadingEvent : MonoBehaviour
{
    public TextMeshProUGUI textToFade;
    public Image imageToFade;

    [SerializeField]
    private S_CurrentEventScriptableObject currentEvent;

    private void OnEnable()
    {
        currentEvent.OnChangingRequirement += FadeMenu;
    }

    private void OnDisable()
    {
        currentEvent.OnChangingRequirement -= FadeMenu;
    }

    private void FadeMenu(S_Requirement newEvent)
    {
        textToFade.text = newEvent.NarrativeDescription;

        // Initial setup: set alpha to zero
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0f);
        imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, 0f);

        // Sequence for fade in and out
        Sequence fadeSequence = DOTween.Sequence();

        // Fade In
        fadeSequence.Append(textToFade.DOFade(1f, 1.0f)); // Fade in text over 1 second
        fadeSequence.Join(imageToFade.DOFade(1f, 1.0f)); // Fade in image simultaneously

        // Delay
        fadeSequence.AppendInterval(1.0f); // Wait for 1 second

        // Fade Out
        fadeSequence.Append(textToFade.DOFade(0f, 1.0f)); // Fade out text over 1 second
        fadeSequence.Join(imageToFade.DOFade(0f, 1.0f)); // Fade out image simultaneously

        // Callback when the sequence is complete
        fadeSequence.OnComplete(() =>
        {
            //  Debug.Log("Fade In and Out complete!");
        });

        // Play the sequence
        fadeSequence.Play();
    }

}
