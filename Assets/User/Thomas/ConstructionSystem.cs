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

        //Spawn object
        if (Input.GetKeyDown(KeyCode.E))
        {
            objectSpawned = SpawnGameObject(hit.point);
        }
        
        //Move object
        if (objectSpawned != null)
        {

            objectSpawned.transform.position = ClampPosition(hit.point, 5);
        }

        //Place Object
        if (Input.GetMouseButtonDown(1))
        {
            objectSpawned = null;
        }

    }
    float RoundToGrid(float valueToRound, float gridSize = 1)
    {
        return Mathf.Round(valueToRound / gridSize) * gridSize;
    }

    Vector3 ClampPosition(Vector3 position, float gridSize)
    {
        float clampedX = RoundToGrid(position.x, gridSize);
        float clampedZ = RoundToGrid(position.z, gridSize);

        
        return new Vector3(clampedX, position.y, clampedZ);
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
