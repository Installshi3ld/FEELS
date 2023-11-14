using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Log Command", menuName = "Developer commands")]
public class S_LogCommand : S_ConsoleCommand
{
    public override bool Processed(string[] args)
    {
        string logText = string.Join(" ", args);

        Debug.Log(logText);

        return true;
    }
}
