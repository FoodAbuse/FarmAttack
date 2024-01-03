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


    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);

        if (!closeGap && !closeGapFollow && !exitGap)
        {
            myAgent.speed = 3;
            myAgent.SetDestination(playerPos.position);
            anim.Play("Walk");
        }

        if(distance > 10)
        {
            closeGap = true;
        }


        if (closeGap)
        {
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
            myAgent.speed = 9;

            myAgent.SetDestination(playerPos.position);
            if (distance < 2.5f)
            {
                closeGapFollow = false;
                exitGap = true;
                anim.SetBool("Dig", false);
                anim.SetBool("Exit", true);
            }

        }

        if(exitGap)
        {
            myAgent.SetDestination(transform.position);

           

            timer += Time.deltaTime;
            if(timer >= 1)
            {
                anim.SetBool("Dig", false);
                anim.SetBool("Exit", false);
                exitGap = false;
            }
        }
    }
}
