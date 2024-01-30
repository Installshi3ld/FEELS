using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dispalay : MonoBehaviour

{
    public GameObject JFDecreasespriteObject;
    public GameObject JFIncreasespriteObject;

    void Start()
    {
        // Set the sprite to inactive initially
        JFDecreasespriteObject.SetActive(false);
        JFIncreasespriteObject.SetActive(false);
    }

    void Update()
    {
        // Check if the sprite is active and if 5 seconds have passed
        if (JFDecreasespriteObject.activeSelf && Time.time >= 5f)
        {
            // Deactivate the sprite after 5 seconds
            JFDecreasespriteObject.SetActive(false);
        }
        if (JFIncreasespriteObject.activeSelf && Time.time >= 5f)
        {
            JFIncreasespriteObject.SetActive(false);
        }
    }

    // Function to make the sprite appear
    public void AppearDecreaseSprite()
    {
        // Activate the sprite when called
        JFDecreasespriteObject.SetActive(true);
    }
    public void AppearIncreaseSprite()
    {
        // Activate the sprite when called
       JFIncreasespriteObject.SetActive(true);
    }
}