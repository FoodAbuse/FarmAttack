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
    public Animator cameraMovementAnimator;

    public bool _isRunning;
    private float idleThreshold = 0f; // Adjust this value based on your preference

    // Hugo's Testing - Freeze heat rotation when Radial Dial is Active
    public bool cameraFrozen;

     public float knockbackForce = 10f; // Strength of the knockback
    public float knockbackDuration = 0.5f; // How long the knockback lasts
    private float knockbackTimer = 0f;
    private Vector3 knockbackDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HeadMovement();

        // Regular movement
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

        // Apply knockback
        if (knockbackTimer > 0)
        {
            // Apply knockback force
            characterController.Move(knockbackDirection * knockbackForce * Time.deltaTime);
            knockbackTimer -= Time.deltaTime; // Decrease the timer
        }

        // Mouse Look
        if (!cameraFrozen)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.Rotate(Vector3.up * mouseX);
            Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        // Move the player using CharacterController
        if (knockbackTimer <= 0)  // Only move if not in knockback state
        {
            characterController.Move((moveSpeed + new Vector3(0f, ySpeed, 0f)) * Time.deltaTime);
        }

        float moveMagnitude = new Vector2(horizontal, vertical).magnitude;
        anim.SetBool("Walking", moveMagnitude > 0);

        if (currentSpeed == sprintSpeed)
        {
            _isRunning = true;
        }
        if (currentSpeed == walkSpeed)
        {
            _isRunning = false;
        }
    }

    // Function to apply knockback
    public void ApplyKnockback(Vector3 attackerPosition)
    {
        // Calculate direction opposite to the attacker position
        knockbackDirection = (transform.position - attackerPosition).normalized;

        // Apply some upward force for more dramatic knockback
        knockbackDirection.y = 0.5f; // Adjust for desired vertical effect

        knockbackTimer = knockbackDuration; // Set knockback duration
    }

    public void HeadMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Map the input to the range [0, 1] with 0 representing left, 0.5 representing idle, and 1 representing right
        float mappedInput = Mathf.InverseLerp(-1f, 1f, horizontalInput);

        // Ensure that mappedInput is never less than 0 or greater than 1
        mappedInput = Mathf.Clamp01(mappedInput);

        cameraMovementAnimator.SetFloat("Horizontal", mappedInput);
    }

}
