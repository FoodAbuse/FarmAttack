using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DonutAIBehaviour : MonoBehaviour
{
    [Header("Healer/Ally Hover")]
    public Transform healTarget;
    public float hoverHeight = 4f;
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;

    [Header("Player Logic")]
    public Transform player;
    public float followDistance = 5f;
    public float rotationSpeed = 5f;
    public Animator animator;

    public Transform donutBody;

    void Update()
    {
        // spin the body
        if (donutBody) donutBody.Rotate(0f, -4f, 0f, Space.Self);

        if (healTarget != null)
        {
            Vector3 targetPos = healTarget.position + Vector3.up * hoverHeight;
            MoveTowards(targetPos, turnSpeed);
            SetAnimatorParameters();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < followDistance)
        {
            Vector3 pos = player.position + Vector3.up * hoverHeight;
            MoveTowards(pos, rotationSpeed);
        }
        else
        {
            RotateTowards(player.position);
        }

        SetAnimatorParameters();
    }

    void MoveTowards(Vector3 pos, float rotSpeed)
    {
        Vector3 dir = pos - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look, rotSpeed * Time.deltaTime);
        }

        Vector3 flat = new Vector3(pos.x, hoverHeight, pos.z);
        transform.position = Vector3.MoveTowards(transform.position, flat, moveSpeed * Time.deltaTime);
    }

    void RotateTowards(Vector3 worldPos)
    {
        Vector3 dir = worldPos - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }

    void SetAnimatorParameters()
    {
        Vector3 localDirection = transform.InverseTransformDirection(player.position - transform.position);
        animator.SetFloat("X", Mathf.Clamp(localDirection.x, -1f, 1f));
        animator.SetFloat("Y", Mathf.Clamp(localDirection.z, -1f, 1f));
    }
}
