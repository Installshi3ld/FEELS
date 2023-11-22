using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gravity Command", menuName = "DeveloperCommand/GravityCommand")]
public class S_GravityCommand : S_ConsoleCommand
{
    public override bool Processed(string[] args)
    {
        if (args.Length != 1)
        {
            return false;
        }

        if (!float.TryParse(args[0], out float value)) { Debug.Log(value); return false; }

        Physics.gravity = new Vector3(Physics.gravity.x, value, Physics.gravity.z);

        return true;
    }
}
