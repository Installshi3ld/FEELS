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

    //Ajout Naudar
    private Coroutine scaleCoroutine;
    //Fin Naudar

    private void Awake()
    {
        S_Timeline.OnRequirementChecked += UpdateCheckBox;
        S_Timeline.OnAfterRequirementChecked += UpdateText;
    }

    private void UpdateCheckBox(S_Requirement currentR)
    {
        toggle.isOn = currentR.HasBeenFulfilled;
    }

    private void UpdateText(S_Requirement currentR)
    {
        textEventRequirement.text = currentR.ConstraintDescription;
        //Ajout Naudar 
        textEventRequirement.color = currentR.HasBeenFulfilled ? Color.green : Color.red;

        if (currentR.HasBeenFulfilled == false)
        {
            // Start a new coroutine to gradually scale textEventRequirement from 2 to 1 in 4 seconds
            textEventRequirement.rectTransform.localScale = new Vector3(2f, 2f, 1f);
            StartCoroutine(ScaleTextOverTime(2f, 1f, 3.5f));
            //textEventRequirement.transform.DOScale(1f, 2.5f);  Dotween
        }
        else
        {
            // Reset the scale immediately when the requirement has been fulfilled
            textEventRequirement.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

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
