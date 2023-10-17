using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class S_Tokens : MonoBehaviour
{
    public float Tokens;
    public TMP_Text tokenValue;
    string variableNumber;

    // Start is called before the first frame update
    void Start()
    {
        string variableNumber = Tokens.ToString();

        tokenValue.text = variableNumber;
        
    }

    public void AddToken()
    {
        Tokens = Tokens + 1;
        string variableNumber = Tokens.ToString();
        tokenValue.text = variableNumber;
    }
    public void UseToken()
    {
        Tokens -- ;
        string variableNumber = Tokens.ToString();
        tokenValue.text = variableNumber;
    }
}
