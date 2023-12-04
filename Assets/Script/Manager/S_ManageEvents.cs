using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.Assertions;
using Unity.VisualScripting;

public class S_ManageEvents : MonoBehaviour
{
    [SerializeField]
    private List<S_PhaseScriptableObject> phases = new List<S_PhaseScriptableObject>();
    /*[SerializeField]//copy
    private List<S_PhaseScriptableObject> phasesList = new List<S_PhaseScriptableObject>();*/

    [SerializeField]
    private float secondsBetweenNewConstraint;

    [SerializeField]
    private int chanceForLifeExperienceToSpawn;

    private int currentPhaseIndex;

    public S_EventTimer eventTimer;

    private bool hasLifeEventBeenPicked;
    private S_LifeExperienceScriptableObject pickedLifeExperience;

    [SerializeField]
    private int chanceForLifeExpToOccur;

    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < phases.Count; i++)
        {
            phasesList.Add(phases[i].MakeCopy());
        }*/

        currentPhaseIndex = 0;

        eventTimer.MaxTime = secondsBetweenNewConstraint;

        StartCoroutine(UpdateEvents());
    }
    private void Update()
    {
        eventTimer.IncreaseTimer(Time.deltaTime); //Normally it would be done in the coroutine (if it was possible) that's why the logic is there
    }

    private void ChangePhaseIndex()//LOGIC HERE NOT CORRECT
    {
        if (phases.Count == currentPhaseIndex + 1)
        {
            Debug.Log("all phases ended");
        }
        else
        {
            currentPhaseIndex++;
            already_done.Clear(); // clear actual requirement list
        }
    }

    private IEnumerator UpdateEvents()
    {
        S_Requirement currentRequirement;

        while (!IsAvailableRequirementListEmpty()) //I never remove the phase from the list
        {
            Debug.Log("Current phase requirement count : " + GetAvailableRequirementsInCurrentPhase());
            Debug.Log("current Phase index : " + currentPhaseIndex);
            eventTimer.StartTimerOver();

            currentRequirement = chooseOneRequirementRandomly();
            //ChooseOrNotLifeExperience();

            if(currentRequirement != null)
            {
                Debug.Log(currentRequirement.NarrativeDescription);
            }

            yield return new WaitForSeconds(secondsBetweenNewConstraint);

           
            if (!currentRequirement.CheckIsRequirementFulfilled()) //If not fulfilled after delay : provoke disaster
            {
                foreach (IDisaster consequence in currentRequirement.LinkedDisaster)
                {
                    Debug.Log("provoke disaster : " + consequence.Description);
                    consequence.ProvoqueDisaster();
                }
            }
            if(hasLifeEventBeenPicked && pickedLifeExperience && !pickedLifeExperience.hasBeenPaid)
            {
                //SET FIRE
            }
        }
    }

    private bool IsAvailableRequirementListEmpty()
    {
        if(GetAvailableRequirementsInCurrentPhase().FirstOrDefault() != null)
        {
            return false;
        }
        return true;
    }

    private void ChooseOrNotLifeExperience()
    {
        int randomInt = Random.Range(0, 99);

        if(randomInt <= chanceForLifeExpToOccur)
        {
            hasLifeEventBeenPicked = true;
            pickedLifeExperience = PickRandomLifeExperience();
            if(pickedLifeExperience != null)
            {
                Debug.Log("Random Life experience have been picked : " + pickedLifeExperience.description);
            }
        }
        else
        {
            hasLifeEventBeenPicked = false;
            Debug.Log("No life experience picked (random chance failed)");
        }
    }

    private S_LifeExperienceScriptableObject PickRandomLifeExperience()
    {
        Debug.Log("PhasesList count " + GetAvailableRequirementsInCurrentPhase().ToList().Count);
        Debug.Log("Current Phase Index " + currentPhaseIndex);

        S_PhaseScriptableObject currentPhaseObject = phases[currentPhaseIndex];

        if(currentPhaseObject.lifeExperiences.Count > 0)
        {
            int index = Random.Range(0, currentPhaseObject.lifeExperiences.Count - 1);
			S_LifeExperienceScriptableObject LifeExperienceToReturn = currentPhaseObject.lifeExperiences[index];
            currentPhaseObject.lifeExperiences.RemoveAt(index);
            ChangePhaseIndex();

            return LifeExperienceToReturn;
        }

        return null;
    }

    List<S_Requirement> already_done = new List<S_Requirement>();

    private IEnumerable<S_Requirement> GetAvailableRequirementsInCurrentPhase()
    {
        var current = phases[currentPhaseIndex];
        foreach(S_Requirement item in current.requirements)
        {
            if (already_done.Contains(item)) continue;

            yield return item;
        }

        yield break;
    }

    private S_Requirement chooseOneRequirementRandomly()
    {
        //IF THERE ARE NO PHASES LEFT SHOULDN T BE CALLED
        //phasesList.Add(phases[currentPhaseIndex].MakeCopy()); // INDEX OUT OF RANGE. DO I EVEN NEED THIS LINE ? CAN T REMEMBER
        S_PhaseScriptableObject currentPhaseObject = phases[currentPhaseIndex];

        Debug.Log("the number of requirements contained in the current phase is " + currentPhaseObject.requirements.Count);

        if (currentPhaseObject.requirements.Count > 0)
        {
            List<S_Requirement> available = GetAvailableRequirementsInCurrentPhase().ToList();

			int index = Random.Range(0, available.Count - 1);
			S_Requirement picked = available[index];
            // currentPhaseObject.requirements.RemoveAt(index); //The event of one pool can't occur multiple times and should be deleted to check 
            already_done.Add(picked);

            //Debug.Log("the number of requirements contained in the current phase is " + currentPhaseObject.requirements.Count + phasesList[currentPhaseIndex].requirements.Count);

            // check condition pour changer de phase
			if (available.Count == 1)
			{
			    ChangePhaseIndex();
			}

			return picked;
        }
        return null;
    }
}
