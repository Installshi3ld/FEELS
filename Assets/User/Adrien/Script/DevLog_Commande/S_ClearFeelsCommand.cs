using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ClearFeels Command", menuName =
"DeveloperCommand/ClearFeelsCommand")]
public class S_CommandClearFeels : S_ConsoleCommand
{
    public List <S_Currencies> currencies;
    public override bool Processed(string[] args)
    {
        string logText = string.Join(" ", args);

        Debug.Log("ClearFeelsWorking");

        foreach (S_Currencies cur in currencies)
        {
            cur.RemoveAmount(100000);
        }
    
        return true;
    }
}
