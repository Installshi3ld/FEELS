using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class S_FeelsUI : MonoBehaviour
{
    
    public S_Currencies joyFeels;
    public S_Currencies angerFeels;
    public S_Currencies fearFeels;
    public S_Currencies sadnessFeels;

    public TMP_Text jFeelsValue;
    public TMP_Text aFeelsValue;
    public TMP_Text fFeelsValue;
    public TMP_Text sFeelsValue;

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


}
