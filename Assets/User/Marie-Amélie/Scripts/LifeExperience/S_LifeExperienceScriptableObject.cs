using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create new Life Experience", menuName = "Life Experience")]
public class S_LifeExperienceScriptableObject : ScriptableObject
{
    [SerializeField]
    public string description;
    [SerializeField]
    private int priceToPayToResolve;
    [SerializeField]
    private S_Currencies feelTypeToPay;
    [SerializeField]
    private int firePropagationTimer;

    [NonSerialized]
    public bool hasBeenPaid;

    private void OnEnable()
    {
        hasBeenPaid = false;
    }

    public S_LifeExperienceScriptableObject MakeCopy()
    {
        S_LifeExperienceScriptableObject clone = CreateInstance<S_LifeExperienceScriptableObject>();

        clone.description = description;
        clone.priceToPayToResolve = priceToPayToResolve;
        clone.feelTypeToPay = feelTypeToPay;
        clone.hasBeenPaid = hasBeenPaid;

        return clone;
    }

    IEnumerator SetWorldOnFire()
    {
        while(hasBeenPaid == false)
        {
            Debug.Log("fire is propagating");
            yield return new WaitForSeconds(firePropagationTimer);
        }
        Debug.Log("end of fire");
    }
}
