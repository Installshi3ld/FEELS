using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pH_ProductionPrototype : MonoBehaviour
{
    // Feels amount
    public int currency1;
    public int currency2;

    // self explanatory
    public int goal;

    // how much Feels will increment initially
    public int increment1;
    public int increment2;

    // building costs and returns
    public int tierOneCost;
    public int tierOneIncrement;

    // timer
    public float incrementDelayAmount;

    public float eventDelayAmount;




    void Start()
    {
        StartCoroutine(incrementTimer());

        StartCoroutine(eventTimer());

        Debug.Log("Goal: " + goal);
    }


    void Update()
    {
                
        // production building simulation
        if (Input.GetKeyDown(KeyCode.Q)&& currency1 >= tierOneCost)
        {
            currency1 -= tierOneCost;
            increment2 += tierOneIncrement;
            Debug.Log("Bought production building2 with currency1");
        }

        if (Input.GetKeyDown(KeyCode.W) && currency2 >= tierOneCost)
        {
            currency2 -= tierOneCost;
            increment1 += tierOneIncrement;
            Debug.Log("Bought production building1 with currency2");
        }

    }

    
    private IEnumerator incrementTimer()
    {
        // every "delay" each currency will increase by incrementAmount

        while (true)
        {
            yield return new WaitForSeconds(incrementDelayAmount);

            currency1 += increment2;
            currency2 += increment1;

            Debug.Log("seconds");
        }

    }

    // event timer
    private IEnumerator eventTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(eventDelayAmount);

            if (currency1 <= goal)
            {
                if (currency2 - 200 <= 0) { currency2 = 0; }

                else
                {
                    currency2 -= 200;

                    Debug.Log("Goal c1 not met, -200c2");                            
                }                

            }

            else
            {
                Debug.Log("Goal met!");
            }
                        
            if (currency2 <= goal)
            {
                if (currency1 - 200 <= 0) { currency1 = 0; }

                else
                {
                    currency1 -= 200;

                    Debug.Log("Goal c2 not met, -200c1");
                }
                                
            }

            else
            {
                Debug.Log("Goal met!");
            }

            goal *= 2;
            Debug.Log("New Goal: " + goal);

        }

    }
}
