using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DonutAIBehaviour : MonoBehaviour
{
    public Transform player;
    public float hoverDistance = 3f;
    public float followDistance = 5f;
    public float rotationSpeed = 5f;
    public Animator animator;
    public NavMeshAgent navMeshAgent;

    public Transform donutBody;

    void Start()
    {
    }

    void Update()
    {
        donutBody.Rotate(0, -4, 0, Space.Self);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < followDistance)
        {
            ChasePlayer();
        }
        else
        {
            RotateTowardsPlayer();
        }

        // Set blend parameters for the Animator Controller
        SetAnimatorParameters();
    }


    void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);

        Quaternion targetRotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void SetAnimatorParameters()
    {
        Vector3 localDirection = transform.InverseTransformDirection(player.position - transform.position);

        animator.SetFloat("X", Mathf.Clamp(localDirection.x, -1f, 1f));
        animator.SetFloat("Y", Mathf.Clamp(localDirection.z, -1f, 1f));
    }
}
