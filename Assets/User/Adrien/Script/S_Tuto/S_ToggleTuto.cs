using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ToggleTuto : MonoBehaviour
{
    public Image imageToToggle;

    public void ToggleVisibility()
    {
        imageToToggle.enabled = !imageToToggle.enabled;
    }
}
