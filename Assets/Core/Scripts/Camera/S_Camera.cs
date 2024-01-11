using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Camera : MonoBehaviour
{
    #region parameters
    [Header("General")]
    public GameObject cam;
    public float MouvementSpeedMinHeight = 15;
    public float MouvementSpeedMaxHeight = 40;
    public float dragSpeed;
    public float BorderLimit = 100;
    public float AnimationSpeed = 5f;
    public LayerMask Floorlayer;

    [Header("Zoom")]
    public float MinimumHeight = 5;
    public float MaximumHeight = 50;
    public float Step = 1.2f;
    public float ZoomSpeed = 5f;
    public float LocalizedZoomStrenght = 1.2f;

    private CameraControlActions cameraActions;
    private InputAction movement;

    #endregion

    private void Awake()
    {
        cameraActions = new CameraControlActions();
    }
    private void OnEnable()
    {
        movement = cameraActions.Camera.MoveCamera;
        cameraActions.Camera.ZoomCamera.performed += ZoomCamera;
        cameraActions.Camera.Enable();
        cam.transform.LookAt(this.transform);
    }


    Vector2 smoothMovementKeyboard;
    bool bCameraDrag = false;

    Vector3 CameraDestination = Vector3.zero;

    Vector3 tmpDestination;
    float tmpCoordZ;
    void Update()
    {
        // Keyboard movement
        if (!bCameraDrag)
        {
            smoothMovementKeyboard = movement.ReadValue<Vector2>();
            CameraDestination += new Vector3(smoothMovementKeyboard.x, 0f, smoothMovementKeyboard.y).normalized * Time.deltaTime * GetCurrentMaxSpeed();
        }

        tmpDestination = this.gameObject.transform.position + CameraDestination * AnimationSpeed * Time.deltaTime;

        //Set camera position
        if (CameraDestination.magnitude > 0.05f)
        {
            if(-BorderLimit < tmpDestination.x && tmpDestination.x < BorderLimit)
                this.gameObject.transform.position += new Vector3(CameraDestination.x * AnimationSpeed * Time.deltaTime, 0, 0);

            if (-BorderLimit < tmpDestination.z && tmpDestination.z < BorderLimit)
                this.gameObject.transform.position += new Vector3(0, 0, CameraDestination.z * AnimationSpeed * Time.deltaTime);

            CameraDestination -= CameraDestination * AnimationSpeed * Time.deltaTime ;
        }
        else if(CameraDestination != Vector3.zero)
        {
            CameraDestination = Vector3.zero;
        }
        //Zoom
        if (zoomDestination.magnitude >= 0.1f && 
            MinimumHeight < cam.transform.position.y + zoomDestination.y * ZoomSpeed * Time.deltaTime && //Not too low
            cam.transform.position.y + zoomDestination.y * ZoomSpeed * Time.deltaTime < MaximumHeight) //Not too High
        {
            cam.transform.position += zoomDestination * ZoomSpeed * Time.deltaTime;
            zoomDestination -= zoomDestination * ZoomSpeed * Time.deltaTime;
            cam.transform.LookAt(this.transform);

            if (!Camera.main.orthographic)
                tmpCoordZ = cam.transform.position.z;
        }
        else if (zoomDestination != Vector3.zero)
        {
            zoomDestination = Vector3.zero;
        }

        DragMouvement();
    }

    Vector3 zoomDestination = Vector3.zero;
    void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        float value = Mathf.Clamp(inputValue.ReadValue<Vector2>().y, -1, 1) * Step;

        if (MinimumHeight < cam.transform.position.y - value && cam.transform.position.y - value < MaximumHeight)
        {
            if (Camera.main.orthographic)
                cam.transform.localPosition = new Vector3(0,MaximumHeight - 0.01f, tmpCoordZ);

            Camera.main.orthographic = false;
            zoomDestination = zoomDestination + new Vector3(0, -value, value);

            /*Localized Zoom
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Floorlayer))
            {
                if (inputValue.ReadValue<Vector2>().y > 0)
                {
                    CameraDestination += (hit.point - this.transform.position).normalized * LocalizedZoomStrenght;
                }
            }*/
        }
        else if (MinimumHeight < cam.transform.position.y - value)
        {
            Camera.main.orthographic = true;
            zoomDestination = Vector3.zero;
            Camera.main.orthographicSize = 45;
            cam.transform.rotation = Quaternion.Euler(90, 0, 0);
            cam.transform.localPosition = new Vector3(0, cam.transform.position.y, 0);
        }
    }

    //___
    float cameraSpeedAlpha;
    float GetCurrentMaxSpeed()
    {
        cameraSpeedAlpha = (cam.transform.position.y - MinimumHeight) /( MaximumHeight - MinimumHeight);
        return Mathf.Lerp(MouvementSpeedMinHeight, MouvementSpeedMaxHeight, cameraSpeedAlpha);
    }

    //___
    Vector2 LastMousePosition = Vector2.zero;
    void DragMouvement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            bCameraDrag = true;
            LastMousePosition = Mouse.current.position.ReadValue();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            bCameraDrag = false;
        }

        if (Input.GetMouseButton(1))
        {
            CameraDestination -= new Vector3(
                Mouse.current.position.ReadValue().x - LastMousePosition.x,
                0,
                Mouse.current.position.ReadValue().y - LastMousePosition.y) / dragSpeed;

            LastMousePosition = Mouse.current.position.ReadValue();
        }
        
    }
}
