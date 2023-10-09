using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TreeOfCons : MonoBehaviour
{
    
    public Material[] material;
    private Camera TreeCamera;
    private Camera FreeCamera;

    public GameObject TreeCam;
    public GameObject FreeCam;

    //Blocks Of Leeves
    public GameObject LeevesBlue;
    public GameObject LeevesRed;
    public GameObject LeevesYellow;
    public GameObject LeevesPurple;

    //Leeves Renderers
    private Renderer LeevesBlueRenderer;
    private Renderer LeevesPurpleRenderer;
    private Renderer LeevesRedRenderer;
    private Renderer LeevesYellowRenderer;


    //Counters for switchers
    public short x ;
    public short y ;
    bool CamSwitched = false;


    private Transform Selection;

    private Transform RedTransform;
    public Ray ray;
    RaycastHit hit;
    //Materials
    Material Blue;
    Material Purple;
    Material Red;
    Material Yellow;
    Material LeevesOff;
    // Start is called before the first frame update
    void Start()
    {

        RedTransform = LeevesRed.GetComponent<Transform>();

        TreeCam.SetActive(false);
        FreeCam.SetActive(true);
        
        TreeCamera = TreeCam.GetComponent<Camera>();
        FreeCamera = FreeCam.GetComponent<Camera>();


        Blue = material[0];
        Purple = material[1];
        Red = material[2];
        Yellow = material[3];
        LeevesOff = material[4];

        //Add Render Components 
        LeevesBlueRenderer = LeevesBlue.GetComponent<Renderer>();
        LeevesPurpleRenderer = LeevesPurple.GetComponent<Renderer>();
        LeevesRedRenderer = LeevesRed.GetComponent<Renderer>();
        LeevesYellowRenderer = LeevesYellow.GetComponent<Renderer>();
        

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0) && CamSwitched) {

             ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Selection = hit.transform;
                if (hit.transform != null)
                {
                    
                    
                    
                    if (hit.transform.gameObject)

                    {
                        Debug.Log("1");
                        LeevesRedRenderer.material = Red;

                    }
                    
                    
                    

                    



                    
                }
            
            }
        }




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

            LeevesBlueRenderer.material = LeevesOff;

            LeevesPurpleRenderer.material = LeevesOff;

            LeevesRedRenderer.material = LeevesOff;

            LeevesYellowRenderer.material = LeevesOff;

            TreeCam.SetActive(false);
            FreeCam.SetActive(true);
            CamSwitched = false;
            Console.WriteLine("2");
            //Material switch

            



        }
    }
}
