using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;
using Unity.VisualScripting;

public class S_FeelsUI : MonoBehaviour
{
    public S_Currencies joyFeels, angerFeels, fearFeels, sadnessFeels, currency;

    public TMP_Text jFeelsValue, aFeelsValue, fFeelsValue, sFeelsValue, t_Info, tokenValue;

    public Color infoTextColor;


    // Start is called before the first frame update
    void Start()
    {
        joyFeels.OnRefreshUi += RefreshUI;
        angerFeels.OnRefreshUi += RefreshUI;
        fearFeels.OnRefreshUi += RefreshUI;
        sadnessFeels.OnRefreshUi += RefreshUI;
        currency.OnRefreshUi += RefreshUI;
        //moodManager.OnChangeUIFeelsColor += ChangeColor;
        RefreshUI();
    }

    public void RefreshUI()
    {
        jFeelsValue.text = joyFeels.Amount.ToString();
        aFeelsValue.text = angerFeels.Amount.ToString();
        fFeelsValue.text = fearFeels.Amount.ToString();
        sFeelsValue.text = sadnessFeels.Amount.ToString();
        tokenValue.text = currency.Amount.ToString();
    }

    public void Info(string msg)
    {
        t_Info.text = msg;
        StartCoroutine("WaitForFadeOut");
    }

    float infoTextAlpha = 1f;
    bool canFade = false;
    IEnumerator WaitForFadeOut()
    {
        infoTextAlpha = 1f;
        canFade = false;
        t_Info.color = new Vector4(infoTextColor.r, infoTextColor.g, infoTextColor.b, infoTextAlpha);
        yield return new WaitForSeconds(1f);
        canFade = true;
        StartCoroutine("FadeOut");
    }
    IEnumerator FadeOut()
    {
        while(canFade)
        {
            yield return new WaitForSeconds(0.02f);
            infoTextAlpha -= 0.01f;
            t_Info.color = new Vector4(infoTextColor.r, infoTextColor.g, infoTextColor.b, infoTextAlpha);
            if (infoTextAlpha < 0)
                canFade = false;
        }  
    }

}
