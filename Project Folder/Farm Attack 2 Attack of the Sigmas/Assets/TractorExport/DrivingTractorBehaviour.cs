using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingTractorBehaviour : MonoBehaviour
{
    public Camera playerCamera;
    public float lookXLimit = 45.0f;
    public float lookSpeed;

    float rotationX = 0;

    public WheelCollider FR;
    public WheelCollider FL;
    public WheelCollider BR;
    public WheelCollider BL;

    public Transform FRTransform;
    public Transform FLTransform;
    public Transform BRTransform;
    public Transform BLTransform;

    public float acceleration;
    public float breaking;

    public float maxTurnAngle;

    float currentSpeed;
    float currentBreakForce;
    float currentTurn;

    public void Update()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, Input.GetAxis("Mouse X"), 0);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            acceleration = 500;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            acceleration = 250;
        }
    }

    public void FixedUpdate()
    {
        currentSpeed = acceleration * Input.GetAxis("Vertical");




        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breaking;
        }
        else
        {
            currentBreakForce = 0;
        }

        FR.motorTorque = currentSpeed;
        FL.motorTorque = currentSpeed;

        FR.brakeTorque = currentBreakForce;
        FL.brakeTorque = currentBreakForce;
        BR.brakeTorque = currentBreakForce;
        BL.brakeTorque = currentBreakForce;

        currentTurn = maxTurnAngle * Input.GetAxis("Horizontal");
        FR.steerAngle = currentTurn;
        FL.steerAngle = currentTurn;

        UpdateWheel(FL, FLTransform);
        UpdateWheel(FR, FRTransform);
        


    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }

}
