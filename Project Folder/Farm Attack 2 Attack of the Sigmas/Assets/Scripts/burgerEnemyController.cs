using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class burgerEnemyController : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public float followDistance = 5f; // Distance at which the enemy starts following the player
    public float followSpeed = 3f; // Speed of normal following
    public float rollSpeed = 6f; // Speed of rolling when player is far away
    public Animator animator; // Reference to the Animator component
    public bool isRolling = false; // Flag to indicate if the burger is currently rolling
    public float returnToNormalDistance = 2f; // Distance threshold to return to normal behavior

    private NavMeshAgent agent;
    private bool isFollowing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
            

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within follow distance
        if (distanceToPlayer <= followDistance)
        {
            isFollowing = true;
            agent.speed = followSpeed;
            agent.SetDestination(player.position);
        }
        else
        {
            isFollowing = false;
            Roll();
        }

        // Update animator parameters based on rolling state
        animator.SetBool("IsRolling", isRolling);
        animator.SetBool("IsFollowing", isFollowing);

        // Rotate the burger mesh towards the direction it's heading
        RotateTowardsDirection(agent.velocity.normalized);

        // Check if the player is close enough to return to normal behavior
        if (distanceToPlayer <= returnToNormalDistance)
        {
            isRolling = false;
        }
    }

    void RotateTowardsDirection(Vector3 direction)
    {
        // Calculate the rotation needed to face the direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // Apply the rotation to the burger mesh
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    void Roll()
    {
        isRolling = true;
        // Set rolling speed
        agent.speed = rollSpeed;

        // Move directly towards the player
        agent.SetDestination(player.position);
    }
}