using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UIGameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject UIMenu;

    private void OnEnable()
    {
        S_Timeline.OnAfterDisaster += SetActiveMenu;
    }
    public void SetActiveMenu(S_Requirement uwu, float delay)
    {
        UIMenu.SetActive(true);
    }
}
