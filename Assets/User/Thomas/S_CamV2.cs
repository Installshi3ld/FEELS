using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_CamV2 : MonoBehaviour
{
    public GameObject cam;
    public float MaxMouvementSpeed = 5;
    [Space]
    public float MinimumHeight = 5;
    public float MaximumHeight = 50;
    public float Step = 1.2f;

    [Space]
    public float keyboardAcceleration = 20f;
    public float keyboardDeceleration = 20f;


    private float _currentSpeed = 0;
    private float _cameraSpeed = 0;

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
    }


    Vector2 smoothMovementKeyboard;
    float smoothCameraAlpha;
    void Update()
    {
        //Keyboard
        if(movement.ReadValue<Vector2>() != Vector2.zero) // Accelerate
        {
            smoothMovementKeyboard = movement.ReadValue<Vector2>();
            _currentSpeed = Mathf.Clamp(_currentSpeed + keyboardAcceleration * Time.deltaTime, 0, MaxMouvementSpeed);
        }
        else //Decelerate
        {
            _currentSpeed = Mathf.Clamp(_currentSpeed - keyboardDeceleration * Time.deltaTime, 0, MaxMouvementSpeed);
        }

        this.gameObject.transform.position += new Vector3(smoothMovementKeyboard.x, 0f, smoothMovementKeyboard.y).normalized * _currentSpeed * Time.deltaTime;


    }


    void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        float value = Mathf.Clamp(inputValue.ReadValue<Vector2>().y, -1, 1) * Step;

        if(MinimumHeight < cam.transform.position.y - value && cam.transform.position.y - value < MaximumHeight)
        cam.transform.position += new Vector3(0, -value, value);
        cam.transform.LookAt(this.transform);
    }
}
