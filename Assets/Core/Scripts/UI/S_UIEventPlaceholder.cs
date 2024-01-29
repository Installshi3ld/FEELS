using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_UIEventPlaceholder : MonoBehaviour
{
    public TextMeshProUGUI textEventDescription;
    public TextMeshProUGUI textEventRequirement;

    [SerializeField]
    private S_CurrentEventScriptableObject currentEvent;

    //Ajout Naudar
    private Coroutine scaleCoroutine;
    //Fin Naudar

    private void OnEnable()
    {
        currentEvent.OnChangingRequirement += UpdateMenu;
    }

    private void OnDisable()
    {
        currentEvent.OnChangingRequirement -= UpdateMenu;
    }

    private void UpdateMenu(S_Requirement currentR)
    {
        textEventDescription.text = currentR.NarrativeDescription;
        textEventRequirement.text = currentR.ConstraintDescription;


        //Ajout Naudar
        // Set the color of textEventRequirement to red

        textEventRequirement.color = Color.red;

        // Stop the previous coroutine if it's still running
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        // Start a new coroutine to gradually scale textEventRequirement to 1 in 3 seconds
        scaleCoroutine = StartCoroutine(ScaleTextOverTime(1f, 3f));
    }

    private IEnumerator ScaleTextOverTime(float targetScale, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startScale = textEventRequirement.rectTransform.localScale;

        while (elapsedTime < duration)
        {
            float scale = Mathf.Lerp(startScale.x, targetScale, elapsedTime / duration);
            textEventRequirement.rectTransform.localScale = new Vector3(scale, scale, 1f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is exactly the target scale
        textEventRequirement.rectTransform.localScale = new Vector3(targetScale, targetScale, 1f);

        // Debug log to confirm the completion of the scaling
        Debug.Log("Text scaling completed.");
    }
    //Fin Naudar
}
