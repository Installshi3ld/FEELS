using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RemoveFeels Command", menuName =
"DeveloperCommand/RemoveFeelsCommand")]
public class S_CommandRemoveFeels : S_ConsoleCommand
{
    public List <S_Currencies> currencies;
    public override bool Processed(string[] args)
    {
        string logText = string.Join(" ", args);

        Debug.Log("RemoveFeelsWorking");

        foreach (S_Currencies cur in currencies)
        {
            cur.SetAmount(0);
        }
    
        return true;
    }
}
