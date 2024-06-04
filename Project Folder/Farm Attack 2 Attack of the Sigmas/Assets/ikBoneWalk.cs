using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ikBoneWalk : MonoBehaviour
{
    public Transform LeftFoot;
    public Transform RightFoot;
    public Transform LeftRaycastShootPos;
    public Transform RightRaycastShootPos;
    public Transform LeftTarget;
    public Transform RightTarget;

    public float moveDistance;
    public float distanceToMove;
    public float smoothSpeed = 5f;

    public LayerMask layerMask;

    private bool leftFootMoving = false;
    private bool rightFootMoving = false;

    void Update()
    {
        MoveFoot(LeftFoot, LeftRaycastShootPos, LeftTarget, ref leftFootMoving);
        MoveFoot(RightFoot, RightRaycastShootPos, RightTarget, ref rightFootMoving);
    }

    void MoveFoot(Transform foot, Transform raycastShootPos, Transform target, ref bool footMoving)
    {
        // Interpolate the position of the foot to the target position smoothly
        foot.position = Vector3.MoveTowards(foot.position, target.position, smoothSpeed * Time.deltaTime);

        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(raycastShootPos.position, raycastShootPos.TransformDirection(Vector3.down), out hit, 5, layerMask))
        {
            moveDistance = Vector3.Distance(target.position, hit.point);

            if (moveDistance > distanceToMove)
            {
                target.position = hit.point;
                footMoving = true;
            }
            else
            {
                footMoving = false;
            }
        }

        // Introduce an offset to the foot's position if the other foot is moving
        if (leftFootMoving && rightFootMoving)
        {
            target.position += new Vector3(0, 0, 0.1f); // Adjust the offset value as needed
        }
    }
}