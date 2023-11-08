using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_EventTimer : MonoBehaviour
{

    public float timerDuration;
    public float currentTimer = 0f;
    

    public Slider slider;

    public TMP_Text eventName;

    //public S_EventScriptableObject scriptMarie;

    // Start is called before the first frame update
    private void Start()
    {
      //  string value = scriptMarie.description;
    }




  
    
    
        
        
        
        // Update is called once per frame
    private void Update()
    {
        
      // eventName.text = scriptMarie.description.ToString();
   

        currentTimer += Time.deltaTime;

        if (currentTimer > timerDuration) 
        {
            currentTimer = 0f;
        }

        float sliderValue = currentTimer / timerDuration;

        slider.value = sliderValue;    

        Debug.Log("Valeur actuelle :" + sliderValue);
     }
}
