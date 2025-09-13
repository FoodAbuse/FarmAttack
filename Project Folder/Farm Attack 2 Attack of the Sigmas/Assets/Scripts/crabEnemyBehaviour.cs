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
    public bool circlePlayer;
    public bool hasAttacked_;

    public Transform playerPos;
    NavMeshAgent myAgent;

    public GameObject slamFX;
    public Transform slamFXTransform;
    public bool startSlam_;

    public Animator anim;
    float timer;
    float timerForAttack;

    public float orbitRadius = 4f;
    public float orbitTurn = 120f;
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if (startSlam_)
        {

            GameObject newSlamFX = Instantiate(slamFX, slamFXTransform.position, slamFXTransform.rotation);
            Destroy(newSlamFX, 2.5f);
            startSlam_ = false;

        }

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        float distance = Vector3.Distance(transform.position, playerPos.position);

        if (!closeGap && !closeGapFollow && !exitGap)
        {

            myAgent.speed = 3;
            myAgent.SetDestination(playerPos.position);

          //  if (!isAttacking && !circlePlayer)
           // {
           //     anim.Play("Walk");
           // }
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

            myAgent.speed = 12;
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

        if(circlePlayer)
        {
            anim.SetBool("Attack", false);

            myAgent.enabled = true;
            isAttacking = false;
            myAgent.speed = 5.5f;

            Vector3 c = playerPos.position;
            Vector3 d = transform.position - c; if (d.sqrMagnitude < 0.01f) d = transform.right;
            Vector3 o = (Quaternion.Euler(0f, orbitTurn * Time.deltaTime, 0f) * d).normalized * orbitRadius;
            myAgent.SetDestination(c + o);

            Vector3 look = playerPos.position - transform.position; look.y = 0f;
            if (look.sqrMagnitude > 0.0001f)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(look), myAgent.angularSpeed * Time.deltaTime);


            timerForAttack += Time.deltaTime;

            if(timerForAttack >= 2.5f)
            {
                circlePlayer = false;
                timerForAttack = 0;
            }
        }

        if(!circlePlayer )
        {
            if (distance < 2)
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
        }

        if(hasAttacked_)
        {
            circlePlayer = true;
            isAttacking = false;
            hasAttacked_ = false;
 
        }

    }

    
}