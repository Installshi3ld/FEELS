using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EventResolutionManager : MonoBehaviour
{
    [SerializeField] private S_VFXManager vfxManager;
    [SerializeField] private S_UIDisaster uiDisaster;

    public void ResolveEvent(FeelType feeltype, S_Requirement currentRequirement)
    {
        StartCoroutine(ResolveEventCoroutine(feeltype, currentRequirement));
    }

    public float delayBetweenEventResolutionPhases;
    IEnumerator ResolveEventCoroutine(FeelType feeltype, S_Requirement currentRequirement)
    {
        uiDisaster.UpdateDisasterUI(currentRequirement);
        yield return new WaitForSeconds(delayBetweenEventResolutionPhases);
        vfxManager.InstantiateCorrectVFX(feeltype);
        yield return new WaitForSeconds(delayBetweenEventResolutionPhases);

    }
}
