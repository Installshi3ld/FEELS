using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIHoverEndTurnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI infoProductionComming; // Reference to the UI Text component to display information
    public Image productionComming;
    public S_BuildingList buildingList;
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
            infoProductionComming.text = "+ " + totalValue.ToString() + " Feels";
            productionComming.gameObject.SetActive(true); // Show the text display
        }
        else
        {
            Debug.LogError("BuildingInfo is not assigned!");
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide information when the pointer exits the UI element
        productionComming.gameObject.SetActive(false); // Hide the text display
    }
}