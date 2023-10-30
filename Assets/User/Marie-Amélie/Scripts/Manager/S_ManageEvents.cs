using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class S_ManageEvents : MonoBehaviour
{
    [SerializeField]
    private List<S_PhaseScriptableObject> phases = new List<S_PhaseScriptableObject>();
    private List<S_PhaseScriptableObject> phasesList = new List<S_PhaseScriptableObject>();

    [SerializeField]
    private float secondsBetweenNewEvent;

    [SerializeField]
    private S_CurrentPhase currentPhase;
    private int currentPhaseIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseIndex = currentPhase.PhaseIndex;

        for (int i = 0; i < phases.Count; i++)
        {
            phasesList.Add(phases[i].MakeCopy());
        }

        StartCoroutine(UpdateEvents());
    }

    private void ChangeIndex()
    {
        if(phasesList[currentPhaseIndex].events.Count <= 0)
        {
            currentPhase.PhaseIndex++;
        }
    }

    private IEnumerator UpdateEvents()
    {
        S_EventScriptableObject currentEvent;

        while (true)
        {
            currentEvent = ChooseOneEventRandomly();

            if (currentEvent)
            {
                currentEvent.applyEvent();
            }

            yield return new WaitForSeconds(secondsBetweenNewEvent);
        }
    }

    private S_EventScriptableObject ChooseOneEventRandomly() //RETURN A RANDOM EVENT CONTAINED IN THE CURRENT PHASE
    {
        S_PhaseScriptableObject currentPhaseObject = phasesList[currentPhaseIndex];

        S_EventScriptableObject EventToReturn;

        if (currentPhaseObject.events.Count > 0)
        {
            int index = Random.Range(0, currentPhaseObject.events.Count - 1);
            EventToReturn = currentPhaseObject.events[index];
            currentPhaseObject.events.RemoveAt(index); //The event of one pool can't occur multiple times and should be deleted to check 
            ChangeIndex();

            return EventToReturn;
        }
        return null;
    }

    private IEnumerator callLifeCycleEveryXSeconds(float seconds)
    {
        ManageLifeCyclesEvents();
        yield return new WaitForSeconds(seconds);
        callLifeCycleEveryXSeconds(seconds);
    }

    private IEnumerator ManageLifeCyclesEvents()
    {
        S_EventScriptableObject chosenEvent = ChooseOneEventRandomly();
        chosenEvent.applyEvent();
        
        Debug.Log(chosenEvent.description);

        yield return null;
    }
}
