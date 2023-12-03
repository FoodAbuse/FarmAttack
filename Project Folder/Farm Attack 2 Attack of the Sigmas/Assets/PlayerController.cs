using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private float ySpeed;
    private float pitch = 0f;

    public Animator anim;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 moveSpeed = transform.TransformDirection(movement) * currentSpeed;

        ySpeed += gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            ySpeed = -0.5f; 

            // Jumping
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
            }
        }

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Move the player using CharacterController
        characterController.Move((moveSpeed + new Vector3(0f, ySpeed, 0f)) * Time.deltaTime);

        float moveMagnitude = new Vector2(horizontal, vertical).magnitude;
        anim.SetBool("Walking", moveMagnitude > 0);
    }
}
