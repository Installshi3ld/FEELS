using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GridCell : MonoBehaviour
{
    public int row;
    public int col;
    public GameObject cubeObject;
    public string additionnalInfo;
    public Color cubeColor;


    public S_GridCell(Color color)
    {
        cubeColor = color;
    }

 
}
