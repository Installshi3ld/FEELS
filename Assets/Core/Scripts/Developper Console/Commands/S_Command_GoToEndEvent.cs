using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TimerEnd Command", menuName =
"DeveloperCommand/TimerEndCommand")]
public class S_TimerEndCommand : S_ConsoleCommand
{
    public S_Timeline eventTimer;
    public override bool Processed(string[] args)
    {
        string logText = string.Join(" ", args);

        Debug.Log("working");
        //eventTimer.secondsBetweenNewConstraint = 5;

        return true;
    }
}
