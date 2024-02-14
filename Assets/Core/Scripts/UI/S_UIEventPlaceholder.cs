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
    private void OnEnable()
    {
        currentEvent.OnChangingRequirement += UpdateMenu;
    }

    private void OnDisable()
    {
        currentEvent.OnChangingRequirement -= UpdateMenu;
    }

    private void UpdateMenu(S_Requirement currentR, float delay)
    {
        StartCoroutine(UpdateWithDelayOrNot(currentR, delay));
    }

    IEnumerator UpdateWithDelayOrNot(S_Requirement currentR, float delay)
    {
        textEventDescription.text = " ";
        textEventRequirement.text = " ";
        yield return new WaitForSeconds(delay);
        textEventDescription.text = currentR.NarrativeDescription;
        textEventRequirement.text = currentR.ConstraintDescription;
    }
}
