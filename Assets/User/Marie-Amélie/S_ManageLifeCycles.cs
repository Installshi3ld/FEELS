using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ManageLifeCycles : MonoBehaviour
{
    [SerializeField]
    private List<S_LifeCyclePhasesScriptableObject> cycles = new List<S_LifeCyclePhasesScriptableObject>();

    [SerializeField]
    private float secondsBetweenLifeCycles;

    private int currentPhase;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(callLifeCycleEveryXSeconds(secondsBetweenLifeCycles));
    }

    private IEnumerator knowCurrentPhase(int currentPhase)
    {

        yield return new WaitForSeconds(currentPhase);
    } 

    private S_LifeCycleEventsScriptableObject chooseOneEventRandomly() //RETURN A RANDOM EVENTS CONTAINED IN THE CUURRENT PHASE
    {
        S_LifeCyclePhasesScriptableObject currentCycle = cycles[currentPhase];
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
