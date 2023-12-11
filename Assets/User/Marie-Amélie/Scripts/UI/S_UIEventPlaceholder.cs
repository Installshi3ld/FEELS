using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_UIEventPlaceholder : MonoBehaviour
{
    public TextMeshProUGUI textEventDescription;
    public TextMeshProUGUI textEventRequirement;

    private void Awake()
    {
        S_Timeline.OnNewEventPicked += UpdateMenu;
    }

    private void UpdateMenu(S_Requirement currentR)
    {
        textEventDescription.text = currentR.NarrativeDescription;
        textEventRequirement.text = currentR.ConstraintDescription;
    }

}
