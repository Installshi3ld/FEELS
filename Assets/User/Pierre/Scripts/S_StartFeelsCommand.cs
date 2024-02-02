using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StartFeels Command", menuName =
"DeveloperCommand/StartFeelsCommand")]
public class S_CommandStartFeels : S_ConsoleCommand
{
    public List <S_Currencies> currencies;
    public override bool Processed(string[] args)
    {
        string logText = string.Join(" ", args);

        Debug.Log("StartFeelsWorking");

        foreach (S_Currencies cur in currencies)
        {
            cur.AddAmount(20);
        }
    
        return true;
    }
}
