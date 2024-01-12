using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour

{


    public GameObject Text;

    void Start()
    {
        Text.SetActive(false);
        Debug.Log("SetActivetoFalse");
    }

    public void OnPointerEnter()
    {
        Text.SetActive(true);
        Debug.Log("MouseOver");
    }

    public void OnPointerExit()
    {
        Text.SetActive(false);
        Debug.Log("MouseExit");
    }

}
