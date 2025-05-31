using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class burgerEnemyController : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f;
    public float followSpeed = 3f;
    public float rollSpeed = 6f;
    public float attackDistance = 1.5f;
    public Animator animator;

    private NavMeshAgent agent;
    private bool isAttacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isAttacking)
        {
            if (distanceToPlayer <= attackDistance)
            {
                Attack();
            }
            else if (distanceToPlayer <= followDistance)
            {
                FollowPlayer();
            }
            else
            {
                Roll();
            }
        }
        else
        {
            // Keep chasing the player during attack
            agent.SetDestination(player.position);
        }

        // True if moving (follow or roll), false if standing still
        bool isMoving = !isAttacking && agent.velocity.magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);

        animator.SetBool("IsRolling", !isAttacking && agent.speed == rollSpeed);
    }

    void FollowPlayer()
    {
        agent.speed = followSpeed;
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void Roll()
    {
        agent.speed = rollSpeed;
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        isAttacking = true;
        // DON'T stop agent here, keep moving so it chases player
        animator.SetTrigger("Attack");
        StartCoroutine(EndAttack());
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(2);

        // Optional: short delay to "recover" or reassess
        yield return new WaitForSeconds(0.3f);

        isAttacking = false;
        agent.isStopped = false;
    }
}