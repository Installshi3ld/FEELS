using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pH_Buildings : MonoBehaviour
{
    public pH_Currencies CurrenciesReference;


    // Start is called before the first frame update



    // Update is called once per frame
    void Update()
    {
        // RED
        // tier 1
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Red", 0);
        }

        // tier 2
        if (Input.GetKeyDown(KeyCode.A))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Red", 1);
        }


        // tier 3
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Red", 2);
        }

        // BLUE
        // tier 1
        if (Input.GetKeyDown(KeyCode.W))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Blue", 0);
        }

        // tier 2
        if (Input.GetKeyDown(KeyCode.S))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Blue", 1);
        }

        // tier 3
        if (Input.GetKeyDown(KeyCode.X))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Blue", 2);
        }

        // YELLOW
        // tier 1
        if (Input.GetKeyDown(KeyCode.E))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Yellow", 0);
        }

        // tier 2
        if (Input.GetKeyDown(KeyCode.D))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Yellow", 1);
        }

        // tier 3
        if (Input.GetKeyDown(KeyCode.C))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Yellow", 2);
        }

        // PURPLE
        // tier 1
        if (Input.GetKeyDown(KeyCode.R))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Purple", 0);
        }

        // tier 2
        if (Input.GetKeyDown(KeyCode.F))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Purple", 1);
        }

        // tier 3
        if (Input.GetKeyDown(KeyCode.V))
        {
            pH_Currencies.Instance.TryIncreaseProduction("Purple", 2);
        }
    }
}
