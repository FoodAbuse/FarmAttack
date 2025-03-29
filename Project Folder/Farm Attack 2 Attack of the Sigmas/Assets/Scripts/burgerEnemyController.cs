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
    public float returnToNormalDistance = 2f;

    private NavMeshAgent agent;
    public bool isFollowing = false;
    public bool isAttacking = false;
    public bool isRolling = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance && !isAttacking)
        {
            Attack();
        }
        else if (distanceToPlayer <= followDistance && !isAttacking)
        {
            FollowPlayer();
        }
        else
        {
            Roll();
        }

        animator.SetBool("IsRolling", isRolling);
        animator.SetBool("IsFollowing", isFollowing);
        animator.SetBool("IsAttacking", isAttacking);

        RotateTowardsDirection(agent.velocity.normalized);

        if (distanceToPlayer <= returnToNormalDistance)
        {
            isRolling = false;
            animator.SetBool("IsRolling", false);

        }
    }

    void FollowPlayer()
    {
        isFollowing = true;
        agent.speed = followSpeed;
        agent.SetDestination(player.position);
    }

    void Roll()
    {
        isRolling = true;
        agent.speed = rollSpeed;
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        isAttacking = true;
        agent.isStopped = true;
        StartCoroutine(EndAttack());
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
        agent.isStopped = false;

        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            isFollowing = true;
        }
    }

    void RotateTowardsDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}