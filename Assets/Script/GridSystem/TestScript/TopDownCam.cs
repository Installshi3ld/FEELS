using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCam : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float scrollSpeed = 10f;

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0) * Time.deltaTime;
        }
    }
}
