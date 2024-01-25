using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class S_UILifeExperiencePrice : MonoBehaviour
{
    [SerializeField] private List<CurrencyUIPanel> currencyUIPanelList;
    [SerializeField] private S_UILifeExperiencePriceScriptableObject UIPriceSO;

    private void OnEnable()
    {
        UIPriceSO.UpdatePriceUILifeExp += UpdatePriceUI;

        foreach (var item in currencyUIPanelList)
        {
            item.currencyUI.SetActive(false);
        }
    }

    private void OnDisable()
    {
        UIPriceSO.UpdatePriceUILifeExp += UpdatePriceUI;
    }


    public void UpdatePriceUI(S_LifeExperienceScriptableObject currentLifeExp)
    {
        if(currentLifeExp == null)
        {
            foreach(var item in currencyUIPanelList)
            {
                item.currencyUI.SetActive(false);
                item.currencyFields.text = "0";
            }
            return;
        }
        foreach(var c in currencyUIPanelList)
        {
            if(c.correspondingCurrency == currentLifeExp.feelTypeToPay)
            {
                c.currencyUI.SetActive(true);
                c.currencyFields.text = currentLifeExp.priceToPayToResolve.ToString();
            }
            else
            {
                c.currencyUI.SetActive(false);
                c.currencyFields.text = "0";
            }
        }
    } 

}

[Serializable]
public struct CurrencyUIPanel
{
    public GameObject currencyUI;
    public TextMeshProUGUI currencyFields;
    public S_Currencies correspondingCurrency;
}


