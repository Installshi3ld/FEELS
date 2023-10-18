using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class S_Tokens : MonoBehaviour
{
    public TMP_Text tokenValue;
    string variableNumber;
    public S_Currencies currency;

    // Start is called before the first frame update
    void Start()
    {
        tokenValue.text = currency.amount.ToString();
    }

    public void AddToken()
    {

        currency.amount += 1;
        tokenValue.text = currency.amount.ToString();

    }
    public void UseToken()
    {
        currency.amount -= 1 ;
        tokenValue.text = currency.amount.ToString();

    }
}
