using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// sqdjkghez
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
    private S_CurrentPhase currentPhase;
    private int currentPhaseIndex;

    public UnityEvent setMax = new UnityEvent();
    public UnityEvent<int> updateSlider = new UnityEvent<int>();

    public S_EventTimer eventTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseIndex = currentPhase.PhaseIndex;

        for (int i = 0; i < phases.Count; i++)
        {
            phasesList.Add(phases[i].MakeCopy());
        }

        if(setMax != null)
        {
            //setMax.Invoke(secondsBetweenNewConstraint);
        }

        eventTimer.MaxTime = secondsBetweenNewConstraint;
        StartCoroutine(UpdateEvents());
    }

    private void Update()
    {
        eventTimer.eventTimerState += Time.deltaTime; //Normally it would be done in the coroutine (if it was possible) that's why the logic is there
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

            if (!currentRequirement.HasBeenFulfilled)
            {
                //provoke disaster
            }
        }
    }

    private S_Requirement chooseOneRequirementRandomly()
    {
        phasesList.Add(phases[currentPhaseIndex].MakeCopy());
        S_PhaseScriptableObject currentPhaseObject = phasesList[currentPhaseIndex];
        S_Requirement RequirementToReturn;

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
}
