using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_UILifeExperience : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] private S_UILifeExpDelegateScriptableObject methodUIRefresh;


    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float delayBetweenEffects = 1f;

    private void OnEnable()
    {
        methodUIRefresh.OnLifeExperienceSpawned += CallShowAndHideText;
    }

    private void OnDisable()
    {
        methodUIRefresh.OnLifeExperienceSpawned -= CallShowAndHideText;
    }


    void Start()
    {
        // Start the initial animation
        StartCoroutine(ShowAndHideText());
    }
    
    private void CallShowAndHideText(bool isLifeExpActive)
    {
        text.text = isLifeExpActive ? "Life experience" : " ";

        if (isLifeExpActive)
        {
            StartCoroutine(ShowAndHideText());
        }
    }
    IEnumerator ShowAndHideText()
    {
        while (methodUIRefresh.IsLifeExperienceActive)
        {
            // Show text with fade-in effect
            yield return FadeTextIn(fadeInDuration);

            // Delay between effects
            yield return new WaitForSeconds(delayBetweenEffects);

            // Hide text with fade-out effect
            yield return FadeTextOut(fadeOutDuration);

            // Repeat the process
            StartCoroutine(ShowAndHideText());
        }

    }

    IEnumerator FadeTextIn(float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = text.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(initialColor, Color.white, elapsedTime / duration);
            yield return null;
        }

        // Ensure the final color is set
        text.color = Color.white;
    }

    IEnumerator FadeTextOut(float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = text.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(initialColor, Color.clear, elapsedTime / duration);
            yield return null;
        }

        // Ensure the final color is set
        text.color = Color.clear;
    }
}
