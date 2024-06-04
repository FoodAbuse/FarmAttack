using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tractorDrivingBehaviour : MonoBehaviour
{
    public float maxSpeed = 20f; // Maximum speed of the car
    public float acceleration = 10f; // Acceleration rate
    public float deceleration = 10f; // Deceleration rate
    public float turnSpeed = 50f; // Turning speed

    private Rigidbody rb;
    private float currentSpeed = 0f;

    public float sensitivity = 5f;

    private float rotationX = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.parent.Rotate(Vector3.up * mouseX);

        float moveInput = Input.GetAxis("Vertical"); // W and S keys by default
        if (moveInput > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (moveInput < 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        Vector3 moveVector = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    void HandleRotation()
    {
        float turnInput = Input.GetAxis("Horizontal"); // A and D keys by default
        float turnAmount = turnInput * turnSpeed * Time.deltaTime;

        Quaternion turnOffset = Quaternion.Euler(0, turnAmount, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}
