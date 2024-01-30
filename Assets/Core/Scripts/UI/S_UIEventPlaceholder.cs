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

    private void UpdateMenu(S_Requirement currentR)
    {
        textEventDescription.text = currentR.NarrativeDescription;
        textEventRequirement.text = currentR.ConstraintDescription;
    }
}
