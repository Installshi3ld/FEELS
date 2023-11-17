using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Log Command", menuName = "DeveloperCommand/PositionCommand")]
public class S_PositionCommand : S_ConsoleCommand
{
    [SerializeField] private GameObject position;
    [SerializeField] private float howLongCheck;

    [NonSerialized] public UnityEvent<GameObject, float> GivePositionCoroutine;

    public override bool Processed(string[] args)
    {
        //Lui passer args[1]

        GivePositionCoroutine.Invoke(position, howLongCheck); //blabla appeler la fonction qui cherche dans le dico si match clef valeur 

        return true;
    }
}
