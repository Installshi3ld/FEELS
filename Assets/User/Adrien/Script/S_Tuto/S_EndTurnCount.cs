using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_EndTurnCount : MonoBehaviour
{
    public int endTurnCount;
    public int valueAnimal,valueMusic,valueFood,valueActivity;
    public int valueFaceSad, valueFaceFear,valueFaceAnger;
    public S_BuildingPoolData buildingPoolData;
    public S_BuildingPoolUI poolUI;
    // Start is called before the first frame update
    int x = 0;
    int y = 1;

    void Awake()
    {
        endTurnCount = 0;
        BuildingPoolStruct ms = buildingPoolData.list[0][0];
        ms.showInUI = true;

    }

    public void EndTurnPressed()
    {
            endTurnCount++;
            Debug.Log(endTurnCount.ToString());
    }

    public void ActivateButton()
    {
        endTurnCount++;
        Debug.Log(endTurnCount.ToString());
        Debug.Log(buildingPoolData.list[0][1].building + "ShowInUI Le mauvais : " + buildingPoolData.list[0][1].showInUI);
        BuildingPoolStruct ms = buildingPoolData.list[x][y];
        Debug.Log(buildingPoolData.list[x][y].building);

        if (endTurnCount == valueFaceSad)
        {
    
                    
                    // Retrieve the element at position [0][1] and modify its showInUI property
                    //buildingPoolData.list[x][y].ChanggeShowInUIStatement(true);
                    ms.showInUI = true;
                    buildingPoolData.list[x][y] = ms;
                    Debug.Log(buildingPoolData.list[0][1].building + "ShowInUI : " + ms.showInUI);
                    y++;

                
        }

        if (endTurnCount == valueFaceFear)
        {
            ms.showInUI = true;
            buildingPoolData.list[x][y] = ms;
            Debug.Log(buildingPoolData.list[0][1].building + "ShowInUI : " + ms.showInUI);
            y++;
        }

        if(endTurnCount == valueFaceAnger)
        {
            ms.showInUI = true;
            buildingPoolData.list[x][y] = ms;
            Debug.Log(buildingPoolData.list[0][1].building + "ShowInUI : " + ms.showInUI);
            x++;
            y = 0;
        }

        if( endTurnCount == valueAnimal)
        {
            ms.showInUI = true;
            buildingPoolData.list[x][y] = ms;
            buildingPoolData.list[x][1] = ms;
            buildingPoolData.list[x][2] = ms;
            buildingPoolData.list[x][3] = ms;
            Debug.Log(buildingPoolData.list[0][1].building + "ShowInUI : " + ms.showInUI);
            x++;
        }

        if(endTurnCount == valueMusic)
        {
            ms.showInUI = true;
            buildingPoolData.list[x][y] = ms;
            buildingPoolData.list[x][1] = ms;
            buildingPoolData.list[x][2] = ms;
            buildingPoolData.list[x][3] = ms;
            Debug.Log(buildingPoolData.list[0][1].building + "ShowInUI : " + ms.showInUI);
            x++;
        }

        if (endTurnCount == valueFood)
        {
            ms.showInUI = true;
            buildingPoolData.list[x][y] = ms;
            buildingPoolData.list[x][1] = ms;
            buildingPoolData.list[x][2] = ms;
            buildingPoolData.list[x][3] = ms;
            Debug.Log(buildingPoolData.list[0][1].building + "ShowInUI : " + ms.showInUI);
            x++;
        }

        if (endTurnCount == valueActivity)
        {
            ms.showInUI = true;
            buildingPoolData.list[x][y] = ms;
            buildingPoolData.list[x][1] = ms;
            buildingPoolData.list[x][2] = ms;
            buildingPoolData.list[x][3] = ms;
            Debug.Log(buildingPoolData.list[0][1].building + "ShowInUI : " + ms.showInUI);
            x++;
        }

        poolUI.RefreshUI();
        Debug.Log("Je refresh bg");
    }
}
