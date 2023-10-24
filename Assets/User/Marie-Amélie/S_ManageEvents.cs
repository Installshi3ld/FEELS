using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class S_ManageEvents : MonoBehaviour
{
    [SerializeField]
    private List<S_PhaseScriptableObject> phases = new List<S_PhaseScriptableObject>();

    [SerializeField]
    private float secondsBetweenNewEvent;

    private int currentPhaseIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseIndex = 0;
        StartCoroutine(UpdateLifePhase());
        StartCoroutine(UpdateEvents());
    }

    private IEnumerator UpdateLifePhase() //sauvegarder déjà le temps écoulé
    {

        while (true)
        {
            yield return new WaitForSeconds(phases[currentPhaseIndex].phaseDuration);

            if (currentPhaseIndex < phases.Count-1)
            {
                currentPhaseIndex++;// en faire un so pour la save
                Debug.Log(phases[currentPhaseIndex].nameOfPhase);
            }

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

    private S_EventScriptableObject ChooseOneEventRandomly() //RETURN A RANDOM EVENTS CONTAINED IN THE CURRENT PHASE
    {
        S_PhaseScriptableObject currentPhase = phases[currentPhaseIndex];
        Debug.Log(currentPhase.events.Count);
        S_EventScriptableObject EventToReturn;

        if (currentPhase.events.Count > 0)
        {
            int index = Random.Range(0, currentPhase.events.Count - 1);
            EventToReturn = currentPhase.events[index];
            currentPhase.events.RemoveAt(index); //The event of one pool can't occur multiple times
            

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
