using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Requirement", menuName = "Requirements/Building")]
public class S_BuildingRequirement : S_Requirement
{
    [SerializeField] private string eventDescription;
    public string narrativeDescription => eventDescription;

    [SerializeField] private string constraintRequirement;
    public string constraintDescription => constraintRequirement;

    [SerializeField] 
    private int numberOfBuildingOfTypeRequired;

    [SerializeField]
    private S_BuildingList buildingsOnMap;


    private bool isFulfilled = false;
    public bool hasBeenFulfilled => isFulfilled;

    public override bool CheckIsRequirementFulfilled()
    {
        UWWUUUUUUU();
        return true;
    }

    private void UWWUUUUUUU()
    {

    }

}
