using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class S_Timeline : MonoBehaviour
{
    [SerializeField]
    private List<S_PhaseScriptableObject> phases = new List<S_PhaseScriptableObject>();

    [SerializeField]
    public float secondsBetweenNewConstraint;

    private int currentPhaseIndex;

    public S_EventTimer eventTimer;

    private bool hasLifeEventBeenPicked;
    private S_LifeExperienceScriptableObject pickedLifeExperience;

    [SerializeField]
    private int chanceForLifeExpToOccur;

    [SerializeField]
    private S_CurrentEventScriptableObject currentEvent;

    S_Requirement currentRequirement;

    public delegate void RefreshFromEvent(S_Requirement currentEvent);
    public static event RefreshFromEvent OnRequirementChecked;
    public static event RefreshFromEvent OnDisasterOccuring;

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseIndex = 0;

        eventTimer.MaxTime = secondsBetweenNewConstraint;

        StartCoroutine(UpdateEvents());
    }
    private void Update()
    {
        eventTimer.IncreaseTimer(Time.deltaTime); //Normally it would be done in the coroutine (if it was possible) that's why the logic is there
        
        if (OnRequirementChecked != null && currentRequirement != null)
        {
            currentRequirement.CheckIsRequirementFulfilled();
            Debug.Log(currentRequirement.HasBeenFulfilled);
            OnRequirementChecked.Invoke(currentRequirement); //Update CheckBox
        }
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
            already_done_requirement.Clear(); // clear actual requirement list
            already_done_lifeExperience.Clear();
        }
    }

    private IEnumerator UpdateEvents()
    {

        while (!IsAvailableRequirementListEmpty())
        {
            /*Debug.Log("Current phase requirement count : " + GetAvailableRequirementsInCurrentPhase());
            Debug.Log("current Phase index : " + currentPhaseIndex);*/

            eventTimer.StartTimerOver();

            currentRequirement = chooseOneRequirementRandomly();

            ChooseOrNotLifeExperience();

            if (currentRequirement != null)
            {
                //Debug.Log(currentRequirement.NarrativeDescription);

                currentEvent.SetNewRequirement(currentRequirement);

                }

            yield return new WaitForSeconds(secondsBetweenNewConstraint);

            if (!currentRequirement.CheckIsRequirementFulfilled()) //If not fulfilled after delay : provoke disaster
            {
                foreach (S_Disaster consequence in currentRequirement.LinkedDisaster)
                {
                    Debug.Log("provoke disaster : " + consequence.Description);

                    if (OnDisasterOccuring != null)
                    {
                        OnDisasterOccuring.Invoke(currentRequirement);
                    }

                    consequence.ProvoqueDisaster();
                }
            }
            else
            {
                foreach(S_Reward reward in currentRequirement.LinkedRewards)
                {
                    reward.GetReward();
                }
            }
            if (hasLifeEventBeenPicked && pickedLifeExperience && !pickedLifeExperience.hasBeenPaid)
            {
                //SET FIRE
            }
        }
    }

    private bool IsAvailableRequirementListEmpty()
    {
        if (GetAvailableRequirementsInCurrentPhase().FirstOrDefault() != null)
        {
            return false;
        }
        return true;
    }

    //LIFE EXPERIENCE

    List<S_LifeExperienceScriptableObject> already_done_lifeExperience = new List<S_LifeExperienceScriptableObject>();

    private IEnumerable<S_LifeExperienceScriptableObject> GetAvailableLifeExperienceInCurrentPhase()
    {
        var current = phases[currentPhaseIndex];

        foreach (S_LifeExperienceScriptableObject item in current.lifeExperiences)
        {
            if (already_done_lifeExperience.Contains(item)) continue;

            yield return item;
        }

        yield break;
    }

    private S_LifeExperienceScriptableObject PickRandomLifeExperience()
    {
        S_PhaseScriptableObject currentPhaseObject = phases[currentPhaseIndex];

        if (currentPhaseObject.requirements.Count > 0)
        {
            List<S_LifeExperienceScriptableObject> available = GetAvailableLifeExperienceInCurrentPhase().ToList();

            int index = Random.Range(0, available.Count - 1);

            if (available.Count > 0)
            {
                S_LifeExperienceScriptableObject picked = available[index];

                already_done_lifeExperience.Add(picked);

                return picked;
            }

            return null;
        }

        return null;
    }

    private void ChooseOrNotLifeExperience()
    {
        int randomInt = Random.Range(0, 99);

        if (randomInt <= chanceForLifeExpToOccur)
        {
            hasLifeEventBeenPicked = true;
            pickedLifeExperience = PickRandomLifeExperience();

            if (pickedLifeExperience != null)
            {
                Debug.Log("Random Life experience have been picked : " + pickedLifeExperience.description);
            }
            else
            {
                Debug.Log("No Life Experience left in the current phase");
            }
        }
        else
        {
            hasLifeEventBeenPicked = false;
            Debug.Log("No life experience picked (random chance failed)");
        }
    }


    // REQUIREMENTS

    List<S_Requirement> already_done_requirement = new List<S_Requirement>();

    private IEnumerable<S_Requirement> GetAvailableRequirementsInCurrentPhase()
    {
        var current = phases[currentPhaseIndex];
        foreach (S_Requirement item in current.requirements)
        {
            if (already_done_requirement.Contains(item)) continue;

            yield return item;
        }

        yield break;
    }

    private S_Requirement chooseOneRequirementRandomly()
    {
        S_PhaseScriptableObject currentPhaseObject = phases[currentPhaseIndex];

        List<S_Requirement> available = GetAvailableRequirementsInCurrentPhase().ToList();

        if (available.Count > 0)
        {
            int index = Random.Range(0, available.Count - 1);
            S_Requirement picked = available[index];
            already_done_requirement.Add(picked);

            if (available.Count == 1)
            {
                ChangePhaseIndex();
            }

            return picked;
        }

        return null;
    }
}


