using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    public Transform foot; // The foot object to move
    public Transform raycastOrigin; // The position from where the raycast is shot

    public Transform foot2; // The foot object to move
    public Transform raycastOrigin2; // The position from where the raycast is shot

    public float moveThreshold = 0.5f; // Distance threshold to trigger movement

    private void Update()
    {
        // Shoot a raycast downwards from the raycast origin
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, Vector3.down, out hit))
        {
            // Check if the raycast hits the ground
            if (hit.collider.CompareTag("World"))
            {
                // Calculate distance between foot and hit point
                float distanceToHitPoint = Vector3.Distance(foot.position, hit.point);

                // If foot is far enough away, move it to hit position
                if (distanceToHitPoint >= moveThreshold)
                {
                    foot.position = hit.point;
                } 
                
                
            } 
            
            // Shoot a raycast downwards from the raycast origin
            RaycastHit hit2;
            if (Physics.Raycast(raycastOrigin2.position, Vector3.down, out hit2))
            {
                // Check if the raycast hits the ground
                if (hit2.collider.CompareTag("World"))
                {
                    // Calculate distance between foot and hit point
                    float distanceToHitPoint2 = Vector3.Distance(foot2.position, hit2.point);

                    // If foot is far enough away, move it to hit position
                    if (distanceToHitPoint2 >= moveThreshold)
                    {
                        foot2.position = hit2.point;
                    }
                }
            }
        }
    }
}
