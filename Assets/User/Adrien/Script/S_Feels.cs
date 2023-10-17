using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class S_Feels : MonoBehaviour
{
    public int joyFeels;
    public int angerFeels;
    public int fearFeels;
    public int sadnessFeels;

    public TMP_Text jFeelsValue;
    public TMP_Text aFeelsValue;
    public TMP_Text fFeelsValue;
    public TMP_Text sFeelsValue;

    // Start is called before the first frame update
    void Start()
    {
        jFeelsValue.text = joyFeels.ToString();
        aFeelsValue.text = angerFeels.ToString();
        fFeelsValue.text = fearFeels.ToString();
        sFeelsValue.text = sadnessFeels.ToString();
    }

    public void AddJFeels()
    {
        joyFeels += 1;
        jFeelsValue.text = joyFeels.ToString();
    }

    public void AddAFeels()
    {
        angerFeels += 1;
        aFeelsValue.text = angerFeels.ToString();
    }

    public void AddFFeels()
    {
        fearFeels += 1;
        fFeelsValue.text = fearFeels.ToString();
    }

    public void AddSFeels()
    {
        sadnessFeels += 1;
        sFeelsValue.text = joyFeels.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
