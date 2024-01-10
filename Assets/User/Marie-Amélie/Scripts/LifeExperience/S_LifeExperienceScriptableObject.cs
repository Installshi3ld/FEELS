using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create new Life Experience", menuName = "Life Experience")]
public class S_LifeExperienceScriptableObject : ScriptableObject
{
    [SerializeField]
    public S_LifeExperience lifeExperience;

    [SerializeField]
    public bool isNarrativeRequirement;

    [SerializeField]
    public string description;
    [SerializeField]
    public int priceToPayToResolve;
    [SerializeField]
    public S_Currencies feelTypeToPay;
}
