using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.Events;

public class S_Timeline : MonoBehaviour
{
    [SerializeField]
    private List<S_PhaseScriptableObject> phases = new List<S_PhaseScriptableObject>();

    [SerializeField]
    public float secondsBetweenNewConstraint;

    [SerializeField]
    private S_UILifeExpDelegateScriptableObject uiLifeExp;

    [SerializeField]
    private S_UILifeExperiencePriceScriptableObject priceLifeExpUI;

    private int currentPhaseIndex;

    public S_EventTimer eventTimer;

    private bool hasLifeEventBeenPicked;

    [SerializeField]
    private int chanceForLifeExpToOccur;

    [SerializeField]
    private S_CurrentEventScriptableObject currentEvent;

    S_Requirement currentRequirement;

    public delegate void RefreshFromEvent(S_Requirement currentEvent);
    public static event RefreshFromEvent OnRequirementChecked;
    public static event RefreshFromEvent OnDisasterOccuring;

    private bool hasBeenPaid = false;

    private int succeededRequirementForThisPhase;

    private S_LifeExperience currentLifeExperience;

    private S_LifeExperienceScriptableObject pickedLifeExperience;
    public S_LifeExperienceScriptableObject PickedLifeExperience
    {
        get
        {
            return pickedLifeExperience;
        }
        private set
        {
            pickedLifeExperience = value;
            uiLifeExp.SetLifeExperienceBool(value);
            priceLifeExpUI.CallDelegate_UpdatePriceUILifeExp(value);
        }
    }

    private bool timerDone = false;

    [SerializeField]
    private S_VFXManager VFXManager;
    private S_UIDisasterImage disasterBlink;

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseIndex = 0;

        eventTimer.MaxTime = secondsBetweenNewConstraint;

        StartCoroutine(UpdateEvents());
    }
    private void Update()
    {
        if (!timerDone)
        {
            eventTimer.IncreaseTimer(Time.deltaTime); //Normally it would be done in the coroutine (if it was possible) that's why the logic is there
        }

        if (OnRequirementChecked != null && currentRequirement != null)
        {
            currentRequirement.CheckIsRequirementFulfilled();

            OnRequirementChecked.Invoke(currentRequirement); //Update CheckBox
        }
    }

    private void TryChangePhaseIndex()//LOGIC HERE SHOULD CHANGE 
    {
        if(succeededRequirementForThisPhase >= phases[currentPhaseIndex].numberOfRequirementToFulfillToSwitchPhase && !(phases.Count == currentPhaseIndex + 1))
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

            if (!PickedLifeExperience) //If not null means that an unresolved one is already on the map
            {
                ChooseOrNotLifeExperience();
            }


            if (hasLifeEventBeenPicked && PickedLifeExperience && !hasBeenPaid)
            {
                AddFireLifeExperience(PickedLifeExperience);
            }

            currentRequirement = chooseOneRequirementRandomly();


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

                    VFXManager.InstantiateCorrectVFX(consequence.feelType);
                }
            }
            else
            {
                succeededRequirementForThisPhase++;

                foreach (S_Reward reward in currentRequirement.LinkedRewards)
                {
                    reward.GetReward();
                }
            }
        }
        timerDone = true;
    }


    public void AddFireLifeExperience(S_LifeExperienceScriptableObject lifeExpScript)
    {
        currentLifeExperience = Instantiate(lifeExpScript.lifeExperience, new Vector3(0, -500, 0), Quaternion.identity);
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
        hasBeenPaid = false;
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
            PickedLifeExperience = PickRandomLifeExperience();

            if (PickedLifeExperience != null)
            {
                Debug.Log("Random Life experience have been picked : " + PickedLifeExperience.description);
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

            TryChangePhaseIndex();

            return picked;
        }

        //TryChangePhaseIndex(); In case there is no one left but can switch ?? Not sure 
        return null;
    }

    public void PayLifeExperience()
    {
        if (PickedLifeExperience == null)
        {
            return;
        }
        if (PickedLifeExperience.feelTypeToPay.HasEnoughFeels(PickedLifeExperience.priceToPayToResolve))
        {
            PickedLifeExperience.feelTypeToPay.RemoveAmount(PickedLifeExperience.priceToPayToResolve);
            currentLifeExperience.SpawnWonder();
            currentLifeExperience.Clear();

            PickedLifeExperience = null;
            Debug.Log("Life experience solved");
        }
        else
        {
            Debug.Log("you do not have enough money");
        }
    }
}


