using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOfCons : MonoBehaviour
{
    
    public Material[] material; 
    Renderer Rend;
    public GameObject TreeCam;
    public GameObject FreeCam;
    public GameObject LeevesBlue;
    public GameObject LeevesRed;
    public GameObject LeevesYellow;
    public GameObject LeevesPurple;
    
    public short x ;
    public short y ;
    bool CamSwitched = false;


    // Start is called before the first frame update
    void Start()
    {
        

        Rend = GetComponent<Renderer>();
        Rend.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {

        //Tree Mode
        if (Input.GetKeyUp(KeyCode.E) && !CamSwitched)
        {
            //Camera Switch
            TreeCam.SetActive(true);
            FreeCam.SetActive(false);
            CamSwitched = true;
            Console.WriteLine("1");
            


        }
        //Free Mode
        else if ((Input.GetKeyUp(KeyCode.E) && CamSwitched))
        {

            TreeCam.SetActive(false);
            FreeCam.SetActive(true);
            CamSwitched = false;
            Console.WriteLine("2");
            //Material switch
            Rend.material = material[x];


        }
    }
}
