using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NextDay : MonoBehaviour
{

    public float redProbability = 0.25f;
    public float blueProbability = 0.5f;
    public float yellowProbability = 0.75f;
    public float purpleProbability = 1f;

    public float angerIncrease;
    public float sadIncrease;
    public float joyIncrease;
    public float fearIncrease;

    public float angerTemp;
    public float sadTemp;
    public float joyTemp;
    public float fearTemp;

    public float TEMP;
    // Start is called before the first frame update
    void Start()
    {
        TEMP = 1f;  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= redProbability)
            {
                angerTemp = (float)Variables.ActiveScene.Get("anger");

                angerTemp = angerTemp + angerIncrease;

                Variables.ActiveScene.Set("anger", angerTemp);

                Debug.Log("Stepped in shit");
            }
            else if (randomValue <= blueProbability)
            {
                sadTemp = (float)Variables.ActiveScene.Get("sad");

                sadTemp = sadTemp + sadIncrease;

                Variables.ActiveScene.Set("sad", sadTemp);

                Debug.Log("Your mom died");
            }
            else if (randomValue <= yellowProbability)
            {
                joyTemp = (float)Variables.ActiveScene.Get("joy");

                joyTemp = joyTemp + joyIncrease;

                Variables.ActiveScene.Set("joy", joyTemp);

                Debug.Log("Woooooooh we ballin!!!");
            }
            else if (randomValue <= purpleProbability)
            {
                fearTemp = (float)Variables.ActiveScene.Get("fear");

                fearTemp = fearTemp + fearIncrease;

                Variables.ActiveScene.Set("fear", fearTemp);

                Debug.Log("I am going to die");
            }
        }

        /* if (Input.GetKeyDown(KeyCode.Space))
        {
        Debug.Log("Suce");

            angerIncrease = (float)Variables.ActiveScene.Get("anger");

            angerIncrease = angerIncrease + 1f;
            Debug.Log(angerIncrease);

            Variables.ActiveScene.Set("anger" , angerIncrease);

        }

        angerTemp = (float)Variables.ActiveScene.Get("anger");

        angerTemp = angerTemp + angerIncrease;

        Variables.ActiveScene.Set("anger", angerTemp);
        */
    }
}

