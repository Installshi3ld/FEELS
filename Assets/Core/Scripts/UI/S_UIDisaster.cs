using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_UIDisaster : MonoBehaviour
{
    public TextMeshProUGUI textDisasterHere;

    [SerializeField]
    private S_CurrentEventScriptableObject currentEvent;

    private void OnEnable()
    {
        textDisasterHere.alpha = 0f;
        S_Timeline.OnDisasterOccuring += UpdateDisasterUI;
    }

    private void OnDisable()
    {
        S_Timeline.OnDisasterOccuring -= UpdateDisasterUI;
    }

    private void UpdateDisasterUI(S_Requirement currentR)
    {
        //Call BlinkText function after a 0 seconds delay || Don't touch that
        Invoke("StartBlinking", 0f);
    }

    void StartBlinking()
    {
        // Utilisez DOTween pour faire apparaître puis disparaître le texte sur une durée totale de 4 secondes
        textDisasterHere.DOFade(1f, 0.75f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            // Called when the animation is over
            HideText();
        });
    }

    void HideText()
    {
        // Use DOTween to make the text disappear in one second
        textDisasterHere.DOFade(0f, 1f).OnComplete(() =>
        {
            //Call when the anim is over
            textDisasterHere.alpha = 0f;
            Debug.Log("Text hidden");
        });
    }
}
