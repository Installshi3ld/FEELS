using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class S_UILifeExperience : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] private S_UILifeExpDelegateScriptableObject methodUIRefresh;

    private void OnEnable()
    {
        methodUIRefresh.OnLifeExperienceSpawned += CallShowAndHideText;
    }

    private void OnDisable()
    {
        methodUIRefresh.OnLifeExperienceSpawned -= CallShowAndHideText;
    }

    
    private void CallShowAndHideText(bool isLifeExpActive)
    {
        text.text = isLifeExpActive ? "Life experience" : " ";

        if (isLifeExpActive)
        {
            ShowAndHideText();
        }
    }

    private void ShowAndHideText()
    {
        //Call BlinkText function after a 0 seconds delay || Don't touch that
        Invoke("StartBlinking", 0f);
    }

    void StartBlinking()
    {
        // Utilisez DOTween pour faire apparaître puis disparaître le texte sur une durée totale de 4 secondes
        text.DOFade(1f, 0.75f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            // Called when the animation is over
            HideText();
        });
    }

    void HideText()
    {
        // Use DOTween to make the text disappear in one second
        text.DOFade(0f, 1f).OnComplete(() =>
        {
            //Call when the anim is over
            text.alpha = 0f;
            Debug.Log("Text hidden");
        });
    }
}
