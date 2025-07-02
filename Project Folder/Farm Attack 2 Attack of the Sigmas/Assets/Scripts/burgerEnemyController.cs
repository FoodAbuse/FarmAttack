using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class burgerEnemyController : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f;
    public float followBuffer = 0.5f; // add this

    public float followSpeed = 3f;
    public float rollSpeed = 6f;
    public float attackDistance = 1.5f;
    public Animator animator;

    private NavMeshAgent agent;
    private bool isAttacking;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform   ;
        agent = GetComponent<NavMeshAgent>();
    }

    private enum State { Idle, Walking, Rolling }
    private State currentState = State.Idle;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isAttacking)
        {
            // Transition logic with hysteresis
            switch (currentState)
            {
                case State.Rolling:
                    if (distanceToPlayer <= followDistance - followBuffer)
                    {
                        currentState = State.Walking;
                    }
                    break;

                case State.Walking:
                    if (distanceToPlayer > followDistance + followBuffer)
                    {
                        currentState = State.Rolling;
                    }
                    break;

                default:
                    currentState = (distanceToPlayer > followDistance + followBuffer) ? State.Rolling : State.Walking;
                    break;
            }

            // Behavior based on state
            if (distanceToPlayer <= attackDistance)
            {
                Attack();
            }
            else if (currentState == State.Walking)
            {
                FollowPlayer();
            }
            else if (currentState == State.Rolling)
            {
                Roll();
            }
        }

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
        if (isAttacking) return;

        isAttacking = true;
        agent.isStopped = true; // Stop moving for the attack
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        float attackDuration = 1.15f;
        float timer = 0f;

        while (timer < attackDuration)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            // Cancel attack if player moves away mid-animation
            if (distance > attackDistance)
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("CancelAttack"); // optional cancel animation trigger
                isAttacking = false;
                agent.isStopped = false;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Attack finished normally
        isAttacking = false;
        agent.isStopped = false;
    }
}