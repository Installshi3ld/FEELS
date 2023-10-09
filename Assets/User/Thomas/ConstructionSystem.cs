using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstructionSystem : MonoBehaviour
{
    public GameObject objectToSpawn;
    private bool isObjectPlaced = false;
    GameObject objectSpawned = null;

    public int roundRange = 60;

    List<List<bool>> gridsUsageStatement = new List<List<bool>>();

    bool debugTiles = false;

    private void Start()
    {
        print(Global.gridSize);
        //Create 2 dimension table
        for(int i = 0; i < roundRange * 2 / Global.gridSize + 1 ; i++)
        {
            
            List<bool> tmpGrid = new List<bool>();

            for (int j = 0; j < roundRange * 2 / Global.gridSize + 1; j++)
                tmpGrid.Add(false);

            gridsUsageStatement.Add(tmpGrid);
        }

        print(gridsUsageStatement.Count);

    }
   


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, roundRange);

        if (debugTiles)
        {
            for (int i = 0; i < gridsUsageStatement.Count; i++)
            {
                for (int j = 0; j < gridsUsageStatement[i].Count; j++)
                {
                    if (gridsUsageStatement[i][j])
                        Gizmos.color = Color.green;
                    else
                        Gizmos.color = Color.red;

                    Gizmos.DrawWireCube(new Vector3(gridsUsageStatement[i].Count / 2 * -Global.gridSize + i * Global.gridSize,
                        0,
                        gridsUsageStatement[j].Count / 2 * -Global.gridSize + j * Global.gridSize), new Vector3(Global.gridSize - 0.6f, Global.gridSize - 0.6f, Global.gridSize - 0.6f));

                }
            }
        }
        
       
    }



    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Mouse raycast
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Vector3 positionDuHit = hit.point;
            
            UnityEngine.Debug.DrawRay(hit.point, hit.normal, Color.blue);

        }

        if (Input.GetKeyDown(KeyCode.F1))
            debugTiles = !debugTiles;

        if (Input.GetKeyDown(KeyCode.F2))
            IncreaseRoundRange(1);
        

        //Spawn object
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectSpawned == null) {
                objectSpawned = SpawnGameObject(Vector3.zero);
            }
            
            
        }
        
        //Move object
        if (objectSpawned != null)
        {
            if(Vector3.Distance(new Vector3(0,0,0), hit.point) <= roundRange)
                objectSpawned.transform.position = Global.ClampPositionToGrid(hit.point);

        }

        //Place Object
        if (Input.GetMouseButtonDown(0))
        {
            if(objectSpawned != null)
            {
                //Get index base in gridUsageStatement based on position
                int indexX = (int)objectSpawned.transform.position.x / Global.gridSize + gridsUsageStatement.Count / 2;
                int indexZ = (int)objectSpawned.transform.position.z / Global.gridSize + gridsUsageStatement.Count / 2;


                if (!gridsUsageStatement[indexX][indexZ])
                {
                    objectSpawned = null;
                    gridsUsageStatement[indexX][indexZ] = true;
                }
            }
        }

        
            

    }

    /// <summary>
    /// The number of tile to add
    /// </summary>
    /// <param name="tilesAmount">The number of tile to add</param>
    void IncreaseRoundRange(int tilesAmount)
    {
        roundRange += tilesAmount * Global.gridSize;

        List<List<bool>> newGridsUsageStatement = new List<List<bool>>();

        for (int i = 0; i < roundRange * 2 / Global.gridSize + 1; i++)
        {

            List<bool> tmpGrid = new List<bool>();

            for (int j = 0; j < roundRange * 2 / Global.gridSize + 1; j++)
                tmpGrid.Add(false);

            newGridsUsageStatement.Add(tmpGrid);
        }

        print(newGridsUsageStatement.Count);

        for (int i = 0; i < newGridsUsageStatement.Count - tilesAmount*2; i++)
        {
            for (int j = 0; j < newGridsUsageStatement[i].Count - tilesAmount * 2; j++)
            {
                
                if (gridsUsageStatement[i][j])
                {
                    newGridsUsageStatement[i + tilesAmount][j + tilesAmount] = true;
                }
            }
        }

        gridsUsageStatement = newGridsUsageStatement;

    }

    GameObject SpawnGameObject(Vector3 spawnPoint)
    {
        if (objectToSpawn != null && spawnPoint != null)
        {
            GameObject tmp = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity);
            return tmp;

        }
        else
        {
            UnityEngine.Debug.LogError("Object to spawn or spawn point not set!");
            return null;
        }

    }
}
