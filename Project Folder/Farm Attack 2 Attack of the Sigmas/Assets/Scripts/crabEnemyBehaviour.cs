using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class crabEnemyBehaviour : MonoBehaviour
{
    public bool isAttacking;
    public bool closeGap;
    public bool closeGapFollow;
    public bool exitGap;
    public Transform playerPos;
    NavMeshAgent myAgent;

    public Animator anim;
    float timer;


    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        float distance = Vector3.Distance(transform.position, playerPos.position);

        if (!closeGap && !closeGapFollow && !exitGap)
        {

            myAgent.speed = 3;
            myAgent.SetDestination(playerPos.position);

            if (distance <= 2)
            {
                myAgent.enabled = false;
                anim.SetBool("Attack", true);
                isAttacking = true;
            }
            if (distance > 2)
            {
                anim.SetBool("Attack", false);
                myAgent.enabled = true;

                isAttacking = false;
            }

            if (!isAttacking)
            {
                anim.Play("Walk");
            }
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

            if (distance < 3 )
            {
                closeGapFollow = false;
                exitGap = true;
                anim.SetBool("Dig", false);
                anim.SetBool("Exit", true);
            }
        }

        if (exitGap)
        {
            GetComponentInChildren<ModelFollowFloorBehaviour>()?.DisableFollowGround();

            myAgent.SetDestination(transform.position);

            timer += Time.deltaTime;
            if (timer >= 1.15f)
            {
                anim.SetBool("Dig", false);
                anim.SetBool("Exit", false);
                GetComponentInChildren<ModelFollowFloorBehaviour>()?.EnableFollowGround();
                exitGap = false;
            }
        }
    }

}