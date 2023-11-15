using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Log Command", menuName = "DeveloperCommand/PositionCommand")]
public class S_PositionCommand : S_ConsoleCommand
{
    [SerializeField] private GameObject position;
    [SerializeField] private float howLongCheck;
    [SerializeField] private UnityEvent<GameObject, float> GivePositionCoroutine;

    public override bool Processed(string[] args)
    {
        string logText = string.Join(" ", args);

        GivePositionCoroutine.Invoke(position, howLongCheck);

        return true;
    }
}
