using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class crabEnemyBehaviour : MonoBehaviour
{
    public bool closeGap;
    public bool closeGapFollow;
    public bool exitGap;
    public Transform playerPos;
    NavMeshAgent myAgent;

    public Animator anim;
    float timer;

    // Reference to the player's knockback script
     PlayerController playerKnockback;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        // Get reference to the player's knockback script
        playerKnockback = playerPos.GetComponent<PlayerController>();
    }

    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        float distance = Vector3.Distance(transform.position, playerPos.position);

        if (!closeGap && !closeGapFollow && !exitGap)
        {
            myAgent.speed = 3;
            myAgent.SetDestination(playerPos.position);
            anim.Play("Walk");
        }

        if (distance > 10)
        {
            closeGap = true;
        }

        if (closeGap)
        {
            GetComponentInChildren<ModelFollowFloorBehaviour>()?.DisableFollowGround();

            myAgent.SetDestination(transform.position);
            anim.SetBool("Dig", true);
            timer += Time.deltaTime;
            if (timer >= .45f)
            {
                closeGapFollow = true;
                closeGap = false;
                timer = 0;
            }
        }

        if (closeGapFollow)
        {
            GetComponentInChildren<ModelFollowFloorBehaviour>()?.DisableFollowGround();

            myAgent.speed = 9;
            myAgent.SetDestination(playerPos.position);

            // If the enemy is close enough to the player, exit and apply knockback
            if (distance < 3)
            {
                closeGapFollow = false;
                exitGap = true;
                anim.SetBool("Dig", false);
                anim.SetBool("Exit", true);

                // Trigger knockback effect when the enemy exits close range
                if (playerKnockback != null)
                {
                    // Call the knockback function passing the enemy's position
                    playerKnockback.ApplyKnockback(transform.position);
                }
            }
        }

        if (exitGap)
        {
            GetComponentInChildren<ModelFollowFloorBehaviour>()?.DisableFollowGround();

            myAgent.SetDestination(transform.position);

            timer += Time.deltaTime;
            if (timer >= 1.5)
            {
                anim.SetBool("Dig", false);
                anim.SetBool("Exit", false);
                GetComponentInChildren<ModelFollowFloorBehaviour>()?.EnableFollowGround();

                exitGap = false;
            }
        }
    }
}