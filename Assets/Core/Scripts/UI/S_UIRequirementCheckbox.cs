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
     //   initialScale = textEventRequirement.Scale;
        textEventRequirement.transform.DOPunchScale(textEventRequirement.transform.localScale, 1f, 1, 1f);
        Debug.Log("TextquibougeRoundOver");
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
