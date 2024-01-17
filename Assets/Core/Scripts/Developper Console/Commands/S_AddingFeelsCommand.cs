using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AddFeels Command", menuName =
"DeveloperCommand/AddFeelsCommand")]
public class S_CommandAddingFeels : S_ConsoleCommand
{
    public List <S_Currencies> currencies;
    public override bool Processed(string[] args)
    {
        string logText = string.Join(" ", args);

        Debug.Log("AddFeelsWorking");

        foreach (S_Currencies cur in currencies)
        {
            cur.AddAmount(50000);
        }
    
        return true;
    }
}
