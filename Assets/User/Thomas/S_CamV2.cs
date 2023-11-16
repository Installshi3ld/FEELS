using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_CamV2 : MonoBehaviour
{
    public GameObject cam;
    public float MouvementSpeedMinHeight = 15;
    public float MouvementSpeedMaxHeight = 40;
    [Space]
    public float MinimumHeight = 5;
    public float MaximumHeight = 50;
    public float Step = 1.2f;
    public float ZoomSpeed = 5f;

    [Space]
    public float keyboardAcceleration = 20f;
    public float keyboardDeceleration = 20f;
    [Space]
    public float dragSpeed = 5f;


    private float _currentSpeed = 0;

    private CameraControlActions cameraActions;
    private InputAction movement;

    

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
    void Update()
    {
        if (!bCameraDrag)
        {
            //Keyboard
            if (movement.ReadValue<Vector2>() != Vector2.zero) // Accelerate
            {
                smoothMovementKeyboard = movement.ReadValue<Vector2>();
                _currentSpeed = Mathf.Clamp(_currentSpeed + keyboardAcceleration * Time.deltaTime, 0, GetCurrentMaxSpeed());
            }
            else //Decelerate
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed - keyboardDeceleration * Time.deltaTime, 0, GetCurrentMaxSpeed());
            }
            this.gameObject.transform.position += new Vector3(smoothMovementKeyboard.x, 0f, smoothMovementKeyboard.y).normalized * _currentSpeed * Time.deltaTime;
        }
        
        //Drag
        if(DragDestination.magnitude > 0.2f)
        {
            this.gameObject.transform.position += DragDestination * dragSpeed * Time.deltaTime;
            DragDestination -= DragDestination * dragSpeed * Time.deltaTime ;
        }

        //Zoom
        if (zoomDestination.magnitude >= 0.1f && 
            MinimumHeight < cam.transform.position.y + zoomDestination.y * ZoomSpeed * Time.deltaTime && //Not too low
            cam.transform.position.y + zoomDestination.y * ZoomSpeed * Time.deltaTime < MaximumHeight) //Not too High
        {
            cam.transform.position += zoomDestination * ZoomSpeed * Time.deltaTime;
            zoomDestination -= zoomDestination * ZoomSpeed * Time.deltaTime;
            cam.transform.LookAt(this.transform);
        }
        else if (zoomDestination != Vector3.zero)
        {
            zoomDestination = Vector3.zero;
        }

        DragMouvementStart();
    }


    Vector3 zoomDestination = Vector3.zero;
    void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        float value = Mathf.Clamp(inputValue.ReadValue<Vector2>().y, -1, 1) * Step;

        if (MinimumHeight < cam.transform.position.y - value && cam.transform.position.y - value < MaximumHeight)
            zoomDestination = zoomDestination + new Vector3(0, -value, value);
    }

    float cameraSpeedAlpha;
    float GetCurrentMaxSpeed()
    {
        cameraSpeedAlpha = (cam.transform.position.y - MinimumHeight) /( MaximumHeight - MinimumHeight);
        return Mathf.Lerp(MouvementSpeedMinHeight, MouvementSpeedMaxHeight, cameraSpeedAlpha);
    }

    Vector3 DragDestination = Vector3.zero;
    Vector2 LastMousePosition = Vector2.zero;
    void DragMouvementStart()
    {
        if (Input.GetMouseButtonDown(1))
        {
            bCameraDrag = true;
            _currentSpeed = 0;
            LastMousePosition = Mouse.current.position.ReadValue();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            bCameraDrag = false;
        }

        if (Input.GetMouseButton(1))
        {
            DragDestination -= new Vector3(
                Mouse.current.position.ReadValue().x - LastMousePosition.x,
                0,
                Mouse.current.position.ReadValue().y - LastMousePosition.y) / 5;

            LastMousePosition = Mouse.current.position.ReadValue();
        }
        
    }
}
