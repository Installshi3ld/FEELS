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
        tokenValue.text = currency.Amount.ToString();
    }

    public void AddToken()
    {

        currency.AddAmount(1);
        tokenValue.text = currency.Amount.ToString();

    }
    public void UseToken()
    {
        currency.RemoveAmount(1);
        tokenValue.text = currency.Amount.ToString();

    }

    public void RefreshScreen()
    {
        tokenValue.text = currency.Amount.ToString();
    }

    public void SetActiveMenu()
    {
        if (!S_GameFunction.isPaused)
        {
            gameObject.SetActive(false);
        }
    }
}
