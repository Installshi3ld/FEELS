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

    //ButtonsInit
    public GameObject LeevesBlueButton;
    public GameObject LeevesRedButton;
    public GameObject LeevesYellowButton;
    public GameObject LeevesPurpleButton;

    //Leeves Renderers
    protected Renderer LeevesBlueRenderer;
    protected Renderer LeevesPurpleRenderer;
    protected Renderer LeevesRedRenderer;
    protected Renderer LeevesYellowRenderer;


    //Counters for switchers

    bool CamSwitched = false;


    private String Selection;

    private Transform RedTransform;
    Ray ray;
    
    //Materials
    Material Blue;
    Material Purple;
    Material Red;
    Material Yellow;
    Material LeevesOff;

    String BlueLeevesName;
    String RedLeevesName;
    String PurpleLeevesName;
    String YellowLeevesName;
    // Start is called before the first frame update
    void Start()
    {

        RedTransform = LeevesRed.GetComponent<Transform>();
        // Cameras Init
        TreeCam.SetActive(false);
        FreeCam.SetActive(true);
        
        TreeCamera = TreeCam.GetComponent<Camera>();
        FreeCamera = FreeCam.GetComponent<Camera>();
        // Leeves Name Init
        BlueLeevesName = LeevesBlueButton.name;
        RedLeevesName = LeevesRedButton.name;
        PurpleLeevesName = LeevesPurpleButton.name;
        YellowLeevesName = LeevesYellowButton.name;
        //Materials Init
        Blue = material[0];
        Purple = material[3];
        Red = material[1];
        Yellow = material[2];
        LeevesOff = material[4];

        //Renderers Init 
        LeevesBlueRenderer = LeevesBlue.GetComponent<Renderer>();
        LeevesPurpleRenderer = LeevesPurple.GetComponent<Renderer>();
        LeevesRedRenderer = LeevesRed.GetComponent<Renderer>();
        LeevesYellowRenderer = LeevesYellow.GetComponent<Renderer>();

        LeevesBlueButton.SetActive(false);
        LeevesPurpleButton.SetActive(false);
        LeevesRedButton.SetActive(false);
        LeevesYellowButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0) && CamSwitched) {
            //RayTrace
             ray = TreeCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.transform != null)
                {
                    Selection = hit.transform.name;

                    // LeevesColoring
                    if (Selection == BlueLeevesName) {
                        LeevesBlueRenderer.material = Blue;
                    
                    
                    
                    }
                    if (Selection == RedLeevesName)
                    {
                        LeevesRedRenderer.material = Red;



                    }

                    if (Selection == PurpleLeevesName)
                    {
                        LeevesPurpleRenderer.material = Purple;



                    }
                    if (Selection == YellowLeevesName)
                    {
                        LeevesYellowRenderer.material = Yellow;



                    }

                    /*if (hit.transform.gameObject)

                    {
                        Debug.Log("1");
                        LeevesRedRenderer.material = Red;

                    }
                    
                    
                    */






                }
            
            }
        }




        //Tree Mode
        if (Input.GetKeyUp(KeyCode.T) && !CamSwitched)
        {


            //Camera Switch
            TreeCam.SetActive(true);
            FreeCam.SetActive(false);
            CamSwitched = true;

            LeevesBlueButton.SetActive(true);
            LeevesPurpleButton.SetActive(true);
            LeevesRedButton.SetActive(true);
            LeevesYellowButton.SetActive(true);
            Console.WriteLine("1");
            


        }
        //Free Mode
        else if ((Input.GetKeyUp(KeyCode.T) && CamSwitched))
        {

            LeevesBlueButton.SetActive(false);
            LeevesPurpleButton.SetActive(false);
            LeevesRedButton.SetActive(false);
            LeevesYellowButton.SetActive(false);

            TreeCam.SetActive(false);
            FreeCam.SetActive(true);
            CamSwitched = false;
            Console.WriteLine("2");
            

            



        }
    }
}
