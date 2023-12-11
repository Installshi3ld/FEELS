using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UIRequirementCheckbox : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;

    private void Awake()
    {
        S_Timeline.OnNewEventPicked += UpdateCheckBox;
    }
    private void UpdateCheckBox(S_Requirement currentR)
    {
        toggle.isOn = currentR.HasBeenFulfilled;
    }
}
