using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelFollowFloorBehaviour : MonoBehaviour
{
    public float castHeight = 2f;
    public float castDistance = 5f;
    public LayerMask groundMask = ~0;
    public bool alignToNormal = true;
    public bool followGround = true;

    [Header("Smoothing")]
    public float positionLerpSpeed = 20f;   // higher = snappier
    public float rotationLerpSpeed = 20f;   // higher = snappier
    public float normalLerpSpeed = 20f;     // higher = matches slope faster
    public float skin = 0.01f;              // tiny lift to avoid z-fighting

    Vector3 smoothedUp = Vector3.up;

    void LateUpdate()
    {
        if (!followGround) return;

        Vector3 origin = transform.position + Vector3.up * castHeight;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, castDistance + castHeight, groundMask, QueryTriggerInteraction.Ignore))
        {
            // Smooth position
            Vector3 targetPos = hit.point + hit.normal * skin;
            float posT = 1f - Mathf.Exp(-positionLerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos, posT);

            if (alignToNormal)
            {
                // Smooth normal to reduce jitter
                float nT = 1f - Mathf.Exp(-normalLerpSpeed * Time.deltaTime);
                smoothedUp = Vector3.Slerp(smoothedUp, hit.normal, nT);

                // Compute target rotation on the smoothed normal, then smooth to it
                Vector3 fwd = Vector3.ProjectOnPlane(transform.forward, smoothedUp).normalized;
                if (fwd.sqrMagnitude < 0.001f) fwd = Vector3.ProjectOnPlane(transform.right, smoothedUp).normalized;
                Quaternion targetRot = Quaternion.LookRotation(fwd, smoothedUp);

                float rotT = 1f - Mathf.Exp(-rotationLerpSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotT);
            }
        }
    }

    public void EnableFollowGround() { followGround = true; }
    public void DisableFollowGround() { followGround = false; }
}