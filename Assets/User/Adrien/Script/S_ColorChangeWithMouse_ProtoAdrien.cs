using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class S_ColorChangeWithMouse_ProtoAdrien : MonoBehaviour
{

    private Renderer cubeRenderer;
    private Color originalColor;
    private Color assignedColor;
    private S_GridManager_ProtoAdrien colorChangeManager;
    private S_GridCell gridCells;

    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color;
    }

    public void Initialize(S_GridManager_ProtoAdrien manager)
    {
        colorChangeManager = manager;
        colorChangeManager.SetActiveColorChangeScript(this);
    }

     void OnMouseDown()
    {
        if (colorChangeManager != null && colorChangeManager.GetTargetColor() != originalColor)
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    { 
        cubeRenderer.material.color = colorChangeManager.GetTargetColor();
    }


    public Color GetAssignedColor()
    {
        return assignedColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
