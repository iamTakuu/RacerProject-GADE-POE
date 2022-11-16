using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
/// <summary>
/// This is a modified version of 'SpawnCampGames' car contoller.
/// You can find that here: https://www.youtube.com/watch?v=TBIYSksI10k; https://github.com/SpawnCampGames/ArcadeCarController
/// </summary>
public class CarController : MonoBehaviour
{
    [Header("Rigid Body")]
    [SerializeField] private Rigidbody motorSphereRb;
    [SerializeField] private Rigidbody carRb;
    [Header("Speed Properties")]
    [SerializeField] private float forwardSpeed = 200f;
    [SerializeField] private float reverseSpeed = 100f;
    [SerializeField] private float turnSpeed = 150f;

    private float currentSpeed;
    [Header("Acceleration Properties")]
    [SerializeField] private float accelerationLerp = 0.003f;
    [SerializeField] private float decelerationLerp = 0.008f;
    [Header("Set your ground to the same layer")]
    public LayerMask groundLayer;
    [Header("Drag Properties")]
    [SerializeField] private float airDrag = 0.1f;
    [SerializeField] private float groundDrag = 4f;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float maxFov = 60f;
    private bool isGrounded; 
    private float moveInput;
    private float turnInput;
    private bool carActive;
    private void Start()
    {
        //Removes sphere from car to stop stupid acceleration
        motorSphereRb.transform.parent = null;
        carRb.transform.parent = null;
        groundDrag = motorSphereRb.drag;
    }

    private void OnEnable()
    {
        if (FindObjectOfType<EventsManager>() == null)
        {
            carActive = true;
            return;
        }
        EventsManager.Instance.ActivateCar += Activate;
        EventsManager.Instance.DeactivateCar += Deactivate;

    }
    private void OnDisable()
    {
        if (FindObjectOfType<EventsManager>() == null)
        {
           return;
        }
        EventsManager.Instance.ActivateCar -= Activate;
        EventsManager.Instance.DeactivateCar -= Deactivate;

    }

    private void Update()
    {
        if (!carActive)
        {
            transform.position = motorSphereRb.transform.position;
            return;
        }
        InputHandler();
        //Keeps the car object to the sphere
        transform.position = motorSphereRb.transform.position;
        TurnHandler();
        GroundCheck();
        motorSphereRb.drag = isGrounded ? groundDrag : airDrag;
        UpdateFOV();
    }
 
    private void UpdateFOV()
    {
        var speed = currentSpeed.Remap(0, forwardSpeed, 25f, maxFov);
        _virtualCamera.m_Lens.FieldOfView = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = carRb.transform.position;
    }

    private void Activate()
    {
        carActive = true;
    }
    private void Deactivate()
    {
        //transform.position = carSphereRb.transform.position;
        carActive = false;
    }
    private void TurnHandler()
    {
        //Rotation of car only allowed when going above
        float newRotation;
        if (currentSpeed > 10)
        {
            newRotation = turnInput * turnSpeed * Time.deltaTime;
        }
        else
        {
            newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        }
        transform.Rotate(0,newRotation,0, Space.World);

    }
    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, out var hit, 1f, groundLayer);
        //adjust car rotation
        
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        
    }
    private void InputHandler()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, forwardSpeed, accelerationLerp);
        }else 
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -reverseSpeed, decelerationLerp);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, accelerationLerp);
        }
        turnInput = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        if (isGrounded && carActive)
        {
            //move the car sphere
            motorSphereRb.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);
        }
        else
        {
            //gravity stuff
            motorSphereRb.AddForce(new Vector3(0, transform.position.y, 0) * -10f);
        }
        carRb.MoveRotation(transform.rotation);
    }
}
