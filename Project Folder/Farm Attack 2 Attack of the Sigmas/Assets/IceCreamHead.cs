using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamHead : MonoBehaviour
{
    public Transform playerTransform; // Player's transform
    public Transform headTransform; // NPC's head transform
    public float maxAngle = 10f; // Maximum angle in degrees
    public float rotationSpeed;

    void Update()
    {
        // Calculate the direction vector from the NPC's head to the player
        Vector3 directionToPlayer = playerTransform.position - headTransform.position;

        // Calculate the angle between the forward direction of the NPC's head and the direction to the player
        float angle = Vector3.Angle(headTransform.forward, directionToPlayer);

        // Check if the angle is within the allowed range
        if (angle > maxAngle)
        {
            // If the angle exceeds the maximum, constrain it to the maximum angle
            angle = maxAngle;
        }
        else if (angle < -maxAngle)
        {
            // If the angle is less than the negative maximum, constrain it to the negative maximum angle
            angle = -maxAngle;
        }

        // Rotate the NPC's head to face the player at the constrained angle
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        targetRotation *= Quaternion.Euler(0, angle, 0); // Apply the constrained angle
        headTransform.rotation = Quaternion.Slerp(headTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
