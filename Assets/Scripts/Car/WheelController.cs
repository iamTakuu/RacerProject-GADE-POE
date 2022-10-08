using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private GameObject[] wheels;
    [SerializeField] private float rotationSpeed;

    [SerializeField]private Rigidbody motor;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var verticalAxis = Input.GetAxisRaw("Vertical");
        var horizontalAxis = Input.GetAxisRaw("Horizontal");
        foreach (var wheel in wheels)
        {
            if (motor.velocity != Vector3.zero && verticalAxis == 0)
            {
                wheel.transform.Rotate(Time.deltaTime * motor.velocity.magnitude * rotationSpeed,0,0, Space.Self);

            }
            else
            {
                wheel.transform.Rotate(Time.deltaTime * verticalAxis * rotationSpeed,0,0, Space.Self);
            }
        }

        switch (horizontalAxis)
        {
            case > 0:
                _animator.SetBool("goingRight", true);
                _animator.SetBool("goingLeft", false);
                break;
            case < 0:
                _animator.SetBool("goingRight", false);
                _animator.SetBool("goingLeft", true);
                break;
            default:
                _animator.SetBool("goingRight", false);
                _animator.SetBool("goingLeft", false);
                break;
        }
    }
}
   