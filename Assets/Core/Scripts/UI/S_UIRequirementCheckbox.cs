using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_UIRequirementCheckbox : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;
    public TMP_Text textEventRequirement;
    public S_ScriptableRounds s_ScriptableRounds;

    private Vector3 initialScale; // Variable pour stocker la scale initiale
    private bool timer;

    //Ajout Naudar
    private Coroutine scaleCoroutine;
    //Fin Naudar

    private void Awake()
    {
        S_Timeline.OnRequirementChecked += UpdateCheckBox;
        s_ScriptableRounds.OnChangedTurn += ShakeRequirementOverRound;
    }

    bool tmp = true;
    private void UpdateCheckBox(S_Requirement currentR)
    {
        toggle.isOn = currentR.HasBeenFulfilled;
        //Ajout Naudar 
       // Debug.Log(textEventRequirement); Le text existe pas quand on restart
        Transform textKiBouge = textEventRequirement.transform;
        textEventRequirement.color = currentR.HasBeenFulfilled ? Color.green : Color.red;

        //SequenceHasBeenFulfilledFalse.Append(textKiBouge.transform.DOScale(2f, 0.1f));
        //SequenceHasBeenFulfilledFalse.Join(textKiBouge.transform.DOScale(1f, 0.5f));

        if (currentR.HasBeenFulfilled == false)
        {
            if (tmp)
            {
                textKiBouge.transform.DOPunchScale(textKiBouge.transform.localScale, 1f, 1, 1f);
                textKiBouge.transform.DOMoveX(280, 2, false);
                tmp = false;
            }
        }
        else
        {
            textKiBouge.transform.DOScale(1f, 0f);
        }
    }


    private void ShakeRequirementOverRound()
    {
        if (!timer) // Ajoutez une condition pour exécuter le code uniquement si timer est false
        {
            timer = true; // Définir timer sur true avant d'effectuer l'animation

            // En utilisant le callback OnComplete de DOTween pour réinitialiser timer
            textEventRequirement.transform.DOPunchScale(textEventRequirement.transform.localScale, 0.3f, 1, 1f)
                .OnComplete(() =>
                {
                    DOTween.Sequence()
                        .AppendInterval(0.1f)
                        .OnComplete(() =>
                        {
                            timer = false; // Réinitialiser timer après le délai
                        });
                });
        }
    }

    // StartCoroutine(ScaleTextOverTime(2f, 1f, 3.5f));
    private IEnumerator ScaleTextOverTime(float startScale, float targetScale, float duration)
   {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float scale = Mathf.Lerp(startScale, targetScale, elapsedTime / duration);
            textEventRequirement.rectTransform.localScale = new Vector3(scale, scale, 1f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is exactly the target scale
        textEventRequirement.rectTransform.localScale = new Vector3(targetScale, targetScale, 1f);
   }
    //Fin Naudar 
}
