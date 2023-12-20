using Sirenix.OdinInspector.Editor.GettingStarted;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tutorial : MonoBehaviour
{
    public GameObject[] Tutorial;
    public bool tutorialEnabled = false;
    public int currentTutorial = 0;

    // Start is called before the first frame update
    void Start()
    {

        



        

    }

    public void tutorialSwitch()
    {
        Tutorial[currentTutorial].SetActive(false);
        currentTutorial++;
        Tutorial[currentTutorial].SetActive(true);

    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
