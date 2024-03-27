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
    private S_UILifeExpDelegateScriptableObject uiLifeExp;

    [SerializeField]
    private S_UILifeExperiencePriceScriptableObject priceLifeExpUI;

    private int currentPhaseIndex;

    public S_EventTimer eventTimer;

    [SerializeField]
    private int chanceForLifeExpToOccur;

    [SerializeField]
    private S_CurrentEventScriptableObject currentEvent;

    S_Requirement currentRequirement;

    //Adrien

    public S_TutoData _TutoData;
    public S_Tuto _Tuto;

    //Fin Adrien

    public delegate void RefreshFromRequirement(S_Requirement currentEvent);
    public static event RefreshFromRequirement OnRequirementChecked;
    public static event RefreshFromRequirement OnPickedRequirement;
    public static event RefreshFromRequirement OnAfterRequirementChecked;
    public static event RefreshFromRequirement OnEndRequirement;
    public delegate void RefreshFromEvent(S_Requirement currentEvent, float delay);
    public static event RefreshFromEvent OnDisasterOccuring;

    private bool hasBeenPaid = false;

    private bool hasLifeEventBeenPicked;

    private int succeededRequirementForThisPhase;

    private float currentDelay;

    [SerializeField] private float UIDelay;

    [SerializeField] private S_ScriptableRounds rounds;

    private S_LifeExperience currentLifeExperience;

    [SerializeField]
    private GameObject successHolder;
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
    private bool isThereLifeExperienceOnMap = false;

    [SerializeField]
    private S_EventResolutionManager resolutionManager;

    private S_UIDisasterImage disasterBlink;

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseIndex = 0;

        rounds.OnChangedRound += UpdateEvents;

        PickNewEvent();
    }

    private void Update()
    {
        if (currentRequirement)
        {
            currentRequirement.CheckIsRequirementFulfilled();
            OnRequirementChecked?.Invoke(currentRequirement); //Update CheckBox
        }
    }

    private void TryChangePhaseIndex()//So, it is changing phase correctly but still pick event from previous phase pool
    {
        if (succeededRequirementForThisPhase >= phases[currentPhaseIndex].numberOfRequirementToFulfillToSwitchPhase && !(phases.Count == currentPhaseIndex + 1))
        {
            currentPhaseIndex++;
            already_done_requirement.Clear(); // clear actual requirement list
            already_done_lifeExperience.Clear();
        }
    }

    private void UpdateEvents()
    {
        if (currentRequirement != null && !currentRequirement.CheckIsRequirementFulfilled()) //If not fulfilled : provoke disaster
        {
            foreach (S_Disaster consequence in currentRequirement.LinkedDisaster)
            {
                Debug.Log("provoke disaster : " + consequence.Description);

                if (OnDisasterOccuring != null)
                {
                    OnDisasterOccuring.Invoke(currentRequirement, 0);
                }

                resolutionManager.ResolveEvent(consequence.feelType, currentRequirement);
                StartCoroutine(DelayDisasterConsequences(resolutionManager.delayBetweenEventResolutionPhases, consequence));

            }

            currentRequirement = null;
            currentDelay = resolutionManager.delayBetweenEventResolutionPhases;
        }
        else
        {
            currentDelay = 0;
        }

        if (currentRequirement != null && currentRequirement.CheckIsRequirementFulfilled()) //If fulfilled : get reward
        {
            succeededRequirementForThisPhase++;

            foreach (S_Reward reward in currentRequirement.LinkedRewards)
            {
                reward.GetReward();
            }

            currentRequirement = null;
            currentDelay = 3;
            StartCoroutine(DelaySuccess(resolutionManager.delayBetweenEventResolutionPhases));

            /*


           //Adrien
            if (!_TutoData.dataBonus)
            {
                Debug.Log("TutoInfo");
                _Tuto.ShowBonusPlacement();
                _TutoData.dataBonus = true;
            }

            Debug.Log("TutoAllo");*/

            //FinAdrien

        }


        if (!PickedLifeExperience)
        {
            ChooseOrNotLifeExperience();
        }

        if (!IsAvailableRequirementListEmpty())
        {
            /*Debug.Log("Current phase requirement count : " + GetAvailableRequirementsInCurrentPhase());
            Debug.Log("current Phase index : " + currentPhaseIndex);*/
        }

    }

    void PickNewEvent()
    {
        TryChangePhaseIndex();
        currentRequirement = chooseOneRequirementRandomly();
        if(currentRequirement != null)//
        {
            currentEvent.SetNewRequirement(currentRequirement, currentDelay);
            OnPickedRequirement?.Invoke(currentRequirement);
        }

    }

    IEnumerator DelayDisasterConsequences(float delay, S_Disaster consequence)
    {
        OnEndRequirement?.Invoke(currentRequirement);
        PickNewEvent();
        yield return new WaitForSeconds(delay);
        consequence.ProvoqueDisaster();
        yield return new WaitForSeconds(delay);
        OnAfterRequirementChecked?.Invoke(currentRequirement);

        //OnRequirementChecked?.Invoke(currentRequirement);

    }

    IEnumerator DelaySuccess(float delay)
    {
        OnEndRequirement?.Invoke(currentRequirement);
        PickNewEvent();
        successHolder.SetActive(true);
        yield return new WaitForSeconds(delay);

        yield return new WaitForSeconds(delay);
        OnAfterRequirementChecked?.Invoke(currentRequirement);
        //OnRequirementChecked?.Invoke(currentRequirement);
        yield return new WaitForSeconds(delay);

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
        Debug.Log("GIVE ME A LIFE EXPERIENCE");
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
                currentLifeExperience = Instantiate(PickedLifeExperience.lifeExperience, new Vector3(0, -500, 0), Quaternion.identity);
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
        Debug.Log(available.Count + " Number of available requirements");
        if (available.Count > 0)
        {
            int index = Random.Range(0, available.Count - 1);
            S_Requirement picked = available[index];
            already_done_requirement.Add(picked);

            picked.DoSomethingAtFirst();//implement for the mechanics of building requirement. The player should build x buildings even if some were already on map
            rounds.numberOfRoundToSwitchEvent = picked.numberOfTurnToFulfill;
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
            isThereLifeExperienceOnMap = false;
            Debug.Log("Life experience solved");
        }
        else
        {
            Debug.Log("you do not have enough money");
        }
    }
}


