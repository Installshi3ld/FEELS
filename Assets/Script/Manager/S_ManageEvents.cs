using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// UWU
/// </summary>
public class S_ManageEvents : MonoBehaviour
{
    [SerializeField]
    private List<S_PhaseScriptableObject> phases = new List<S_PhaseScriptableObject>();
    [SerializeField]//copy
    private List<S_PhaseScriptableObject> phasesList = new List<S_PhaseScriptableObject>();

    [SerializeField]
    private float secondsBetweenNewConstraint;

    [SerializeField]
    private int chanceForLifeExperienceToSpawn;

    [SerializeField]
    private S_CurrentPhase currentPhase;
    private int currentPhaseIndex;

    public S_EventTimer eventTimer;

    private bool hasLifeEventBeenPicked;

    [SerializeField]
    private int chanceForLifeExpToOccur;

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseIndex = currentPhase.PhaseIndex;

        for (int i = 0; i < phases.Count; i++)
        {
            phasesList.Add(phases[i].MakeCopy());
        }

        eventTimer.MaxTime = secondsBetweenNewConstraint;
        StartCoroutine(UpdateEvents());
    }

    private void Update()
    {
        eventTimer.IncreaseTimer(Time.deltaTime); //Normally it would be done in the coroutine (if it was possible) that's why the logic is there
    }

    private void ChangeIndex()
    {
        Debug.Log("trying switch");

        if(phasesList[currentPhaseIndex].requirements.Count <= 0)
        {
            currentPhase.PhaseIndex++;
            currentPhaseIndex++;

            Debug.Log("have switched, the current phase is " + currentPhase.PhaseIndex++);
        }
    }

    private IEnumerator UpdateEvents()
    {
        S_Requirement currentRequirement;

        while (true)
        {
            eventTimer.StartTimerOver();

            currentRequirement = chooseOneRequirementRandomly();
            Debug.Log(currentRequirement.NarrativeDescription);

            yield return new WaitForSeconds(secondsBetweenNewConstraint);

           
            if (!currentRequirement.CheckIsRequirementFulfilled()) //If not fulfilled after delay : provoke disaster
            {
                foreach (IDisaster consequence in currentRequirement.LinkedDisaster)
                {
                    Debug.Log("provoke disaster : " + consequence.Description);
                    consequence.ProvoqueDisaster();
                }
            }
        }
    }

    private void chooseOrNotLifeEvent()
    {
        int randomInt = Random.Range(0, 99);

        if(randomInt <= chanceForLifeExpToOccur)
        {
            Debug.Log("ture");
        }
    }

    private S_Requirement chooseOneRequirementRandomly()
    {
        phasesList.Add(phases[currentPhaseIndex].MakeCopy());
        S_PhaseScriptableObject currentPhaseObject = phasesList[currentPhaseIndex];
       // Le Cirque Medrano d'Adrien c'est par ici
       //S_Requirement RequirementToReturn;
       //
        Debug.Log("the number of requirements contained in the current phase is " + currentPhaseObject.requirements.Count);

        if (currentPhaseObject.requirements.Count > 0) //ne rentre pas là 
        {
            int index = Random.Range(0, currentPhaseObject.requirements.Count - 1);
            RequirementToReturn = currentPhaseObject.requirements[index];
            currentPhaseObject.requirements.RemoveAt(index); //The event of one pool can't occur multiple times and should be deleted to check 
            ChangeIndex();

            return RequirementToReturn;
        }
        return null;
    }
    // A Partir de la Adrien fais son numéro de Cirque : TPC !

    public S_Requirement RequirementToReturn;
    
}
