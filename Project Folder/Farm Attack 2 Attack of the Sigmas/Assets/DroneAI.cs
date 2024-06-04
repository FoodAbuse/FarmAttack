using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    // Following properties
    public float MinDistance = 3f;
    public float MaxDistance = 1f;
    public float Speed = 3f;
    public Transform Player;

    // Bobbing and rotating properties
    public Vector3 bobAmount;
    public Vector3 rotateAmount;
    public Vector3 bobSpeed;
    public Vector3 rotateSpeed;

    private Vector3 startPos;
    private Vector3 startRot;

    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
    }

    void Update()
    {
        // Following behavior
        if (Vector3.Distance(transform.position, Player.position) >= MinDistance)
        {
            Vector3 follow = Player.position;
            follow.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, follow, Speed * Time.deltaTime);
        }

        // Bobbing and rotating behavior
        Vector3 bobbingPosition = new Vector3(
            bobAmount.x * Mathf.Sin(Time.time * bobSpeed.x),
            bobAmount.y * Mathf.Sin(Time.time * bobSpeed.y),
            bobAmount.z * Mathf.Sin(Time.time * bobSpeed.z)
        );

        Vector3 rotatingAngles = new Vector3(
            rotateAmount.x * Mathf.Sin(Time.time * rotateSpeed.x),
            rotateAmount.y * Mathf.Sin(Time.time * rotateSpeed.y),
            rotateAmount.z * Mathf.Sin(Time.time * rotateSpeed.z)
        );

        // Apply bobbing and rotation relative to current position and rotation
        transform.localPosition = startPos + bobbingPosition;
        transform.localEulerAngles = startRot + rotatingAngles;
    }
}
