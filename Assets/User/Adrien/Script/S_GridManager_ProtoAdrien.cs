using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_GridManager_ProtoAdrien: MonoBehaviour
{
    public int rows = 5;
    public int columns = 5;
    public Camera mainCamera;
    public GameObject cellPrefab;
    public Color objectColor;
    private S_GridCell[,] gridCells;
    public float cellSize = 1.2f;


    // 0 = null | 1 = Joy | 2 = Sad | 3 = Fear | 4 = Anger
    public int buildingSelection;

    private S_ColorChangeWithMouse_ProtoAdrien activeColorChangeScript;
    private S_CheckPositionBuildings_ProtoAdrien activeCheckScript;


    void Start()
    {
        CreateGrid();
        AdjustCamera();
       
    }

    void CreateGrid()
    {
        gridCells = new S_GridCell[rows, columns];
        for (int row = 0; row < rows; row++)
            for (int col = 0; col < columns; col++)
            {
                float xOffset = col * cellSize;
                float yOffset = row * cellSize;


                Vector3 position = new Vector3(xOffset, yOffset, 0);
                GameObject cube = Instantiate(cellPrefab, position, Quaternion.identity, transform) ;

            
                cube.GetComponent<MeshRenderer>().material.color = objectColor;


                S_GridCell cell = new S_GridCell(objectColor)
                {
                    row = row,
                    col = col,
                    cubeObject = cube,
                    additionnalInfo = "Additional Info " + row + "-" + col,
                };

                gridCells[row, col] = cell;
                

                S_ColorChangeWithMouse_ProtoAdrien 

                colorChangeScript = cube.AddComponent<S_ColorChangeWithMouse_ProtoAdrien>();

                colorChangeScript.Initialize(this);

                S_CheckPositionBuildings_ProtoAdrien

                checkPositionScript = cube.AddComponent<S_CheckPositionBuildings_ProtoAdrien>();
                checkPositionScript.Initialize(this);
            }
        int rowIndex = 2;
        int columnIndex = 3;
        Color cellColor = gridCells[rowIndex, columnIndex].cubeColor;
        Debug.Log("Additional Info " + "for cell (" + rowIndex + "," + columnIndex +"):" + gridCells[rowIndex, columnIndex].additionnalInfo);
    }

    void AdjustCamera()
    {
       
        mainCamera.orthographicSize = Mathf.Max(rows + cellSize * rows, columns + cellSize * columns);
        mainCamera.transform.position = new Vector3((rows-1) * 0.5f, (columns-1) * 0.5f, -5f);
    }

 

    public void SetActiveColorChangeScript(S_ColorChangeWithMouse_ProtoAdrien colorChangeScript)
    {
        activeColorChangeScript = colorChangeScript;
    }

    public void SetActiveCheckScript(S_CheckPositionBuildings_ProtoAdrien checkPositionScript)
    {
        activeCheckScript =  checkPositionScript;
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
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }

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

  //  void ChangeColor()
  // {
        // Color gridCells.cubeColor = GetTargetColor();
  // }

    void HandleRightClick()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))

        {
            GameObject clickedObject = hit.collider.gameObject;

            foreach (S_GridCell cell in gridCells) 
                {
                    if ( cell.cubeObject == clickedObject)
                    {
                        // Access the color of the clicked cube
                        Color clickedColor = cell.cubeColor;
                        Debug.Log("Color of the clicked cube: " + clickedColor);
                        return; // Stop searching once the cube is found
                    }
                }
            }
        }
    }
