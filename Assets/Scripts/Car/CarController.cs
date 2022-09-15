using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Rigid Body")]
    [SerializeField] private Rigidbody carSphereRb;
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
    
    private bool isGrounded; 
    private float moveInput;
    private float turnInput;
    private bool carActive;
    
    private void Start()
    {
        //Removes sphere from car to stop stupid acceleration
        carSphereRb.transform.parent = null; 
    }

    private void OnEnable()
    {
        EventsManager.Instance.ActivateCar += Activate;
    }
    private void OnDisable()
    {
        EventsManager.Instance.ActivateCar -= Activate;
    }

    private void Update()
    {
        if (!carActive) return;
        InputHandler();
        //Keeps the car object to the sphere
        transform.position = carSphereRb.transform.position;
        TurnHandler();
        GroundCheck();
        carSphereRb.drag = isGrounded ? groundDrag : airDrag;
    }

    private void Activate()
    {
        carActive = true;
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
        if (isGrounded)
        {
            //move the car sphere
            carSphereRb.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);
        }
        else
        {
            //gravity stuff
            carSphereRb.AddForce(new Vector3(0, transform.position.y, 0) * -10f);
        }
    }
}
