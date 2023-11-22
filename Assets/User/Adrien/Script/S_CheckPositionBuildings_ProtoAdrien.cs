using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckPositionBuildings_ProtoAdrien : MonoBehaviour
{
    public S_ColorChangeWithMouse_ProtoAdrien colorChangeManager;

    private S_GridManager_ProtoAdrien checkManager;

    Color colorNotToCheck = new Color(0f,0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void Initialize(S_GridManager_ProtoAdrien manager)
    {
        checkManager = manager;
        checkManager.SetActiveCheckScript(this);
    }

    void OnRightMouseDown()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnRightMouseDown();

            {
              //S_ColorChangeWithMouse_ProtoAdrien colorChangeScript = GetComponent<Collider>().GetComponent<S_ColorChangeWithMouse_ProtoAdrien>();
              //  if (colorChangeScript != null)
              //  {
              //      Color blockColor = colorChangeScript.GetAssignedColor();
                   // if (blockColor != colorNotToCheck ) 
                   // {
             //           Debug.Log("Color of the block under the mouse: " + blockColor);
                   // }
                    
             //   }
             //  else
             //   {
             //       Debug.LogWarning("The object under the mouse does not have a ColorChangerWithMouse script.");
             //   }
            }
        }
    }
}
