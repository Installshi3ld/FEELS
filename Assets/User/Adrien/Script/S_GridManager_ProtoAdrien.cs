using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_GridManager_ProtoAdrien: MonoBehaviour
{

    public int gridSizeX = 30;
    public int gridSizeY = 30;
    public float cellSize = 1.0f;
    public float spacingCell = 0.1f;
    public Camera mainCamera;


  
    // 0 = null | 1 = Joy | 2 = Sad | 3 = Fear | 4 = Anger
    public int buildingSelection;

    private S_ColorChangeWithMouse_ProtoAdrien activeColorChangeScript;


    void Start()
    {
        CreateGrid();
        AdjustCamera();
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                //Calculate position based on grid size and cell size
                float posX = x * (cellSize + spacingCell);
                float posY = y * (cellSize + spacingCell);

                //Create a GameObject at the calculated position
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                cube.transform.position = new Vector3(posX, posY, 0);

                S_ColorChangeWithMouse_ProtoAdrien 

                colorChangeScript = cube.AddComponent<S_ColorChangeWithMouse_ProtoAdrien>();

                colorChangeScript.Initialize(this);
            }
    }

    void AdjustCamera()
    {
        float gridWidth = gridSizeX * (cellSize + spacingCell);
        float gridHeight = gridSizeY * (cellSize + spacingCell);
        mainCamera.orthographicSize = Mathf.Max(gridWidth, gridHeight) * 0.5f;
        mainCamera.transform.position = new Vector3((gridWidth-1) * 0.5f, (gridHeight-1) * 0.5f, -15f);
    }

 

    public void SetActiveColorChangeScript(S_ColorChangeWithMouse_ProtoAdrien colorChangeScript)
    {
        activeColorChangeScript = colorChangeScript;
    }

    public Color GetTargetColor()
    { 
        if ( buildingSelection == 1)
        {
            return Color.yellow ; 
        }
        else if ( buildingSelection == 2)
        {
            return  Color.blue;
        }
        else if( buildingSelection == 3)
        {
            return Color.black;
        }
        else if (buildingSelection == 4)
        {
            return Color.red;
        }
        else
        {
            return Color.white;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buildingSelection = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buildingSelection = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buildingSelection = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            buildingSelection = 4;
        }
     
    }
}
