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

   // public TMP_Text eventName;

   // private S_EventScriptableObject scriptMarie;

    // Start is called before the first frame update
    private void Start()
    {
    //    scriptMarie = GetComponent<S_EventScriptableObject>();
     //   string value = scriptMarie.description;
    }



    //public void UpdateTextFromSO(S_EventScriptableObject scriptableObject)
    //{
      //  eventName.text = scriptableObject.description;
    //}
    
    
        
        
        
        // Update is called once per frame
    private void Update()
    {
       

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
