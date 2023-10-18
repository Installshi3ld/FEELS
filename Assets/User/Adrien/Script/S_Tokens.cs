using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class S_Tokens : MonoBehaviour
{
    public int tokens;
    public TMP_Text tokenValue;
    string variableNumber;

    // Start is called before the first frame update
    void Start()
    {
       

        tokenValue.text = tokens.ToString();
        
    }

    public void AddToken()
    {
        print(tokens);
        tokens +=1;
        tokenValue.text = tokens.ToString();

      

    }
    public void UseToken()
    {
        print(tokens);
        tokens -= 1 ;
        
        tokenValue.text = tokens.ToString();

       
    }
}
//   string variableNumber = Tokens.ToString();
 //       tokenValue.text = variableNumber;