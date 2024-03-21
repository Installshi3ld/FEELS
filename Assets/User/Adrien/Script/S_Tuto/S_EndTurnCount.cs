using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_EndTurnCount : MonoBehaviour
{
    public int endTurnCount;
    private int valueAnimal,valueMusic,valueFood,valueActivity;
    private int valueFaceSad, valueFaceFear,valueFaceAnger;
    private S_BuildingPoolData buildingPoolData;

    // Start is called before the first frame update
    void Start()
    {
        endTurnCount = 0;
    }

    public void EndTurnPressed()
    {
            endTurnCount++;
            Debug.Log(endTurnCount.ToString());
    }

    public void ActivateButton()
    {
        if (endTurnCount == valueFaceSad)
        {
            // Check if the list has any elements
            if (buildingPoolData.list.Count > 0)
            {
                // Check if the sub-list at index 0 has any elements
                if (buildingPoolData.list[0].Count > 1)
                {
                    // Retrieve the element at position [0][1] and modify its showInUI property
                  //  buildingPoolData.list[0][1].showInUI = true;
                }
                else
                {
                    Debug.LogError("Sub-list at index 0 doesn't have enough elements.");
                }
            }
            else
            {
                Debug.LogError("Main list doesn't have any elements.");
            }
        }

        if (endTurnCount == valueFaceFear)
        {

        }

        if(endTurnCount == valueFaceAnger)
        {

        }

        if( endTurnCount == valueAnimal)
        {

        }

        if(endTurnCount == valueMusic)
        {

        }
    }
}
