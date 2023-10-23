using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;
using Unity.VisualScripting;

public class S_FeelsUI : MonoBehaviour
{
    
    public S_Currencies joyFeels, angerFeels, fearFeels, sadnessFeels;

    public TMP_Text jFeelsValue, aFeelsValue, fFeelsValue, sFeelsValue, t_Info;

    public Color infoTextColor;


    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
    }

    public void AddJFeels(S_Currencies feels)
    {
        feels.amount += 1;
        RefreshUI();
    }

    public void RefreshUI()
    {
        jFeelsValue.text = joyFeels.amount.ToString();
        aFeelsValue.text = angerFeels.amount.ToString();
        fFeelsValue.text = fearFeels.amount.ToString();
        sFeelsValue.text = sadnessFeels.amount.ToString();
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
