using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_ConsoleCommand : ScriptableObject, IConsoleCommand
{
    [SerializeField] private string commandWord = string.Empty;

    string IConsoleCommand.CommandWord => commandWord;

    public abstract bool Processed(string[] args);
}
