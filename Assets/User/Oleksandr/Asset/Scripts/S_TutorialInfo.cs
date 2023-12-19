using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class S_TutorialInfo : MonoBehaviour
{

    public short id;
    public bool isActive;
    public string description;
    public Image TutorialBackground;
    public TextMeshProUGUI TutorialText;

}
