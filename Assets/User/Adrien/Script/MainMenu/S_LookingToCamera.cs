using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Start()
    {
        // Trouver la direction vers la caméra
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;

        // Faire en sorte que le sprite regarde la caméra
        transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}