using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsoleCommand
{
    string CommandWord { get; }
    bool Processed(string[] args);
}
