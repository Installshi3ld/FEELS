using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthographicSizePlus : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            Camera.main.orthographicSize = Camera.main.orthographicSize - 1f;

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            Camera.main.orthographicSize = Camera.main.orthographicSize + 1f;
    }
}
