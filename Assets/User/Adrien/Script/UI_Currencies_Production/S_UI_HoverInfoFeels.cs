using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIHoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI infoCurrentProductionDisplay, valueNextTurn, currentValue; // Reference to the UI Text component to display information
    public Image currentProdUI,nextTurnProd;
    public S_BuildingList buildingList;
    public S_Currencies currencies;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buildingList != null)
        {
            float totalValue = 0f;

            foreach (S_BuildingData buildingInfo in buildingList.builidingsInfos)
            {
                totalValue += buildingInfo.building._FeelAssignationBuilding.currentProduction;
            }

            // Display information when the pointer enters the UI element
            infoCurrentProductionDisplay.text = "+ " + totalValue.ToString() + "/rounds";
            currentProdUI.gameObject.SetActive(true); // Show the text display
            currentValue.text = currencies.amount.ToString();
            valueNextTurn.text = (currencies.amount + totalValue).ToString();
            nextTurnProd.gameObject.SetActive(true);   

        }
        else
        {
            Debug.LogError("BuildingInfo is not assigned!");
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide information when the pointer exits the UI element
        currentProdUI.gameObject.SetActive(false); // Hide the text display

        nextTurnProd.gameObject.SetActive(true);
    }
}