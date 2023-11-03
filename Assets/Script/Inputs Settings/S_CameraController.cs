using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_CameraController : MonoBehaviour
{
    private CameraControlActions cameraActions;
    private InputAction movement;
    private Transform cameraTransform;

    //Horizontal motion
    [SerializeField]
    private float maxSpeed = 5f;
    private float speed;
    [SerializeField]
    private float acceleration = 10f;
    [SerializeField]
    private float damping = 15f;

    //Vertical motion
    [SerializeField]
    private float stepSize = 2f;
    [SerializeField]
    private float zoomDampening = 7.5f;
    [SerializeField]
    private float minHeight = 5f;
    [SerializeField]
    private float maxHeight = 50f;
    [SerializeField]
    private float zoomSpeed = 2f;

    //Screen edge motion
    [SerializeField]
    [Range(0f, 01f)]
    private float edgeTolerance = 0.05f;
    [SerializeField]
    private bool useScreenEdge = true;
    [SerializeField]
    private float CameraClamp = 100f;

    private Vector3 targetPosition;

    private float zoomHeight;

    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;

    private Vector3 dragStartPosition;

    Vector3 startDrag;

    private void Awake()
    {
        cameraActions = new CameraControlActions();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;

    }

    private void OnEnable()
    {
        zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(this.transform);

        lastPosition = this.transform.position;
        movement = cameraActions.Camera.MoveCamera;
        cameraActions.Camera.ZoomCamera.performed += ZoomCamera;
        cameraActions.Camera.Enable();

    }

    private void OnDisable()
    {
        cameraActions.Disable();
    }

    private void UpdateVelocity()
    {
        horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
        horizontalVelocity.y = 0;
        lastPosition = this.transform.position;
    }

    private void Update()
    {
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, -CameraClamp, CameraClamp),
            this.transform.position.y,
            Mathf.Clamp(this.transform.position.z, -CameraClamp, CameraClamp));

        GetKeyboardMovement(); //Getting inputs

        if (useScreenEdge)
        {
            CheckMouseAtScreenEdge();
        }

        DragCamera();

        UpdateVelocity();

        UpdateCameraPosition();

        UpdateBasePosition();
    }

    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight() + movement.ReadValue<Vector2>().y * GetCameraForward();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
        {
            targetPosition += inputValue;
        }
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0;

        return right;
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;

        return forward;
    }

    private void UpdateBasePosition()
    {
        if (Time.timeScale >= 0.01f)
        {
            if (targetPosition.sqrMagnitude > 0.1f)
            {
                speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
                transform.position += targetPosition * speed * Time.deltaTime;
            }
            else
            {
                horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
                transform.position += horizontalVelocity * Time.deltaTime;
            }

            targetPosition = Vector3.zero;
        }
    }


    private void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        float value = -inputValue.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(value) > 0.1f)
        {
            zoomHeight = cameraTransform.localPosition.y + value * stepSize;
            if (zoomHeight < minHeight)
            {
                zoomHeight = minHeight;
            }
            if (zoomHeight > maxHeight)
            {
                zoomHeight = maxHeight;
            }
        }

        // Adjust maxSpeed based on zoomHeight
        maxSpeed = 10f + (zoomHeight - minHeight) / (maxHeight - minHeight) * (100f - 10f); //modify 100 and 10 to change the scales
    }

    private void UpdateCameraPosition()
    {
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
        zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
        cameraTransform.LookAt(this.transform);
    }

    private void CheckMouseAtScreenEdge()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 moveDirection = Vector3.zero;

        if (mousePosition.x < edgeTolerance * Screen.width)
        {
            moveDirection += -GetCameraRight();
        }
        else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
        {
            moveDirection += GetCameraRight();
        }

        if (mousePosition.y < edgeTolerance * Screen.height)
        {
            moveDirection += -GetCameraForward();
        }
        else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
        {
            moveDirection += GetCameraForward();
        }

        targetPosition += moveDirection;
    }

    private void DragCamera() // DEPENDS ON MOUSE SENSITIVITY. CAN BE VERY AGRESSIVE IF DPI TOO HIGH
    {
        if(!Mouse.current.rightButton.isPressed)
        {
            return;
        }

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(plane.Raycast(ray, out float distance))
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                startDrag = ray.GetPoint(distance);
            }
            else
            {
                targetPosition += startDrag - ray.GetPoint(distance);
            }
        }
    }



}
