using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_InputFieldController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    public void KeepActive()
    {
        if (inputField)
        {
            inputField.ActivateInputField();
        }
        
    }

    void Start()
    {
        KeepActive();
    }
}
