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

    private float angerTemp;
    private float sadTemp;
    private float joyTemp;
    private float fearTemp;

    public float cycleCount;

    private float TEMP;
    // Start is called before the first frame update
    void Start()
    {
        TEMP = 1f;
        cycleCount = 0;
        NextCycle();

        cycleCount++;
        Debug.Log("Day: " + cycleCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextCycle();

            cycleCount++;
            Debug.Log("Day: " + cycleCount);
        }
    }

    public float NextCycle()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= redProbability)
        {
            angerTemp = (float)Variables.ActiveScene.Get("anger");

            angerTemp = angerTemp + angerIncrease;

            Variables.ActiveScene.Set("anger", angerTemp);

            Debug.Log("Stepped in shit (+15 anger).");

            return (float)Variables.ActiveScene.Get("anger");
        }
        else if (randomValue <= blueProbability)
        {
            sadTemp = (float)Variables.ActiveScene.Get("sad");

            sadTemp = sadTemp + sadIncrease;

            Variables.ActiveScene.Set("sad", sadTemp);

            Debug.Log("Your mom died (+15 sad).");

            return (float)Variables.ActiveScene.Get("sad");
        }
        else if (randomValue <= yellowProbability)
        {
            joyTemp = (float)Variables.ActiveScene.Get("joy");

            joyTemp = joyTemp + joyIncrease;

            Variables.ActiveScene.Set("joy", joyTemp);

            Debug.Log("Woooooooh we ballin!!! (+15 joy)");

            return (float)Variables.ActiveScene.Get("joy");
        }
        else if (randomValue <= purpleProbability)
        {
            fearTemp = (float)Variables.ActiveScene.Get("fear");

            fearTemp = fearTemp + fearIncrease;

            Variables.ActiveScene.Set("fear", fearTemp);

            Debug.Log("I am going to die (+15 fear).");

            return (float)Variables.ActiveScene.Get("fear");
        }
        // will never return this anyway
        return 0;
    }
}

