using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class burgerEnemyController : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f;
    public float followBuffer = 0.5f;
    public float followSpeed = 3f;
    public float rollSpeed = 6f;
    public float attackDistance = 1.5f;
    public Animator animator;

    public AudioClip BiteSFX;
    public AudioSource soundSource;

    private NavMeshAgent agent;
    private bool isAttacking;

    public enum TargetIntent { Player, Planter }
    public TargetIntent intent = TargetIntent.Planter;

    private Transform PlanterTarget;
    private Transform currentTarget;

    public float aggroRange = 8f;
    public float deAggroRange = 12f;

    private enum State { Idle, Walking, Rolling }
    private State currentState = State.Idle;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlanterTarget = FindClosestPlanter();
        currentTarget = (intent == TargetIntent.Player) ? player : PlanterTarget;

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        // Dynamically re-target based on proximity
        if (intent == TargetIntent.Planter)
        {
            if (distToPlayer < aggroRange)
                currentTarget = player;
            else if (distToPlayer > deAggroRange)
                currentTarget = PlanterTarget;
        }

        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

        if (!isAttacking)
        {
            switch (currentState)
            {
                case State.Rolling:
                    if (distanceToTarget <= followDistance - followBuffer)
                        currentState = State.Walking;
                    break;

                case State.Walking:
                    if (distanceToTarget > followDistance + followBuffer)
                        currentState = State.Rolling;
                    break;

                default:
                    currentState = (distanceToTarget > followDistance + followBuffer) ? State.Rolling : State.Walking;
                    break;
            }

            if (distanceToTarget <= attackDistance)
            {
                Attack();
            }
            else if (currentState == State.Walking)
            {
                MoveToTarget(followSpeed);
            }
            else if (currentState == State.Rolling)
            {
                MoveToTarget(rollSpeed);
            }
        }

        bool isMoving = !isAttacking && agent.velocity.magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsRolling", !isAttacking && agent.speed == rollSpeed);
    }

    void MoveToTarget(float speed)
    {
        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(currentTarget.position);
    }

    void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;
        agent.isStopped = true;
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
            float distance = Vector3.Distance(transform.position, currentTarget.position);

            if (distance > attackDistance)
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("CancelAttack");
                isAttacking = false;
                agent.isStopped = false;
                soundSource.PlayOneShot(BiteSFX, 1);



                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Attack completed
        isAttacking = false;
        agent.isStopped = false;
    }

    Transform FindClosestPlanter()
    {
        GameObject[] Planters = GameObject.FindGameObjectsWithTag("Planter");
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject c in Planters)
        {
            float dist = Vector3.Distance(transform.position, c.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = c.transform;
            }
        }

        return closest;
    }
}