using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelActiver : MonoBehaviour
{

    public Collider Trauma;
    public Button PanelButton;

    public void Start()
    {
        PanelButton.enabled = false;
    }
    void OnTriggerEnter(Collider collision)

    {
       if (collision.gameObject.tag == "trauma")
        {
            Debug.Log("touche");
           PanelButton.enabled = true;
        }


    }
}
