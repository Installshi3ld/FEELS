using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LeavesProperties 
{
    public GameObject leaveToColor;
    public Button button;
    public Material material;
    public Renderer renderer;
    

    public void Setup()
    {
        renderer = leaveToColor.GetComponent<Renderer>();
        

    }
}
