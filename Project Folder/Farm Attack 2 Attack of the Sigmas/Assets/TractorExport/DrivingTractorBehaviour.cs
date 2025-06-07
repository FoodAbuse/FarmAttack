using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingTractorBehaviour : MonoBehaviour
{
    public enum ControlMode { Keyboard, Buttons }
    public enum Axel { Front, Rear }

    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    public ControlMode control;
    public float maxAcceleration = 30f;
    public float brakeAcceleration = 50f;
    public float turnSensitivity = 1f;
    public float maxSteerAngle = 30f;
    public Vector3 _centerOfMass;
    public List<Wheel> wheels;

    float moveInput, steerInput;
    Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
    }

    void Update()
    {
        if (control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    public void MoveInput(float input) => moveInput = input;
    public void SteerInput(float input) => steerInput = input;

    void Move()
    {
        float torque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        foreach (var wheel in wheels)
            wheel.wheelCollider.motorTorque = torque;
    }

    void Steer()
    {
        float steerAngle = steerInput * turnSensitivity * maxSteerAngle;
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, steerAngle, 0.6f);
        }
    }

    void Brake()
    {
        float brakeTorque = (Input.GetKey(KeyCode.Space) || moveInput == 0) ? 300 * brakeAcceleration * Time.deltaTime : 0f;
        foreach (var wheel in wheels)
            wheel.wheelCollider.brakeTorque = brakeTorque;
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
            wheel.wheelModel.transform.SetPositionAndRotation(pos, rot);
        }
    }
    }
