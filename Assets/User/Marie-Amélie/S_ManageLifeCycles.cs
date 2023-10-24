using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class S_ManageLifeCycles : MonoBehaviour
{
    [SerializeField]
    private List<S_LifeCyclePhasesScriptableObject> phases = new List<S_LifeCyclePhasesScriptableObject>();

    [SerializeField]
    private float secondsBetweenLNewPhase;

    [SerializeField]
    private float secondsBetweenNewEvent;

    private int currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = 0;
        //StartCoroutine(callLifeCycleEveryXSeconds(secondsBetweenLNewPhase));
        StartCoroutine(updateLifePhase());
    }

    private IEnumerator updateLifePhase() //sauvegarder déjà le temps écoulé
    {

        while (true)
        {
            yield return new WaitForSeconds(phases[currentPhase].phaseDuration);

            if (currentPhase < phases.Count-1)
            {
                currentPhase++;// en faire un so pour la save
                Debug.Log(phases[currentPhase].nameOfPhase);
            }

        }
        
    } 

    private S_LifeCycleEventsScriptableObject chooseOneEventRandomly() //RETURN A RANDOM EVENTS CONTAINED IN THE CURRENT PHASE
    {
        S_LifeCyclePhasesScriptableObject currentCycle = phases[currentPhase];
        int index = Random.Range(0, currentCycle.events.Count - 1);

        return currentCycle.events[index];
    }

    private IEnumerator callLifeCycleEveryXSeconds(float seconds)
    {
        manageLifeCyclesEvents();
        yield return new WaitForSeconds(seconds);
        callLifeCycleEveryXSeconds(seconds);
    }

    private IEnumerator manageLifeCyclesEvents()
    {
        S_LifeCycleEventsScriptableObject chosenEvent = chooseOneEventRandomly();
        chosenEvent.applyLifeCycles();
        
        Debug.Log(chosenEvent.description);

        yield return null;
    }
}
