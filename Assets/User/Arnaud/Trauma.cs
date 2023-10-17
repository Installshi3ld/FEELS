using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trauma : MonoBehaviour
{
    public GameObject cube;


    public void ActivateGravity()
    {
        cube.GetComponent<Rigidbody>().useGravity = true;
    }



}
