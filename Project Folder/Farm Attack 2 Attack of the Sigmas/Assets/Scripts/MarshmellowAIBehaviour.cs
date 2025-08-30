using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarshmellowAIBehaviour : MonoBehaviour
{

    public bool doHop;
    public GameObject vomitGO;
    public Transform player;
    public Transform vomitPos;
    public int minVomitDistance = 5;
    public int maxVomitDistance = 10;
    public float vomitCooldown = 2f;


    float vomitTimer;

    NavMeshAgent myAgent;
    float distanceFromPlayer;

    public Animator anim;

    void Start()
    {

        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (doHop)
        {
            myAgent.isStopped = false;
        }
        else if (!doHop)
        {
            myAgent.isStopped = true;
        }

        myAgent.destination = player.position;

        // Vomit attack logic
        vomitTimer += Time.deltaTime;
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);


        if (distanceFromPlayer >= minVomitDistance && distanceFromPlayer <= maxVomitDistance && vomitTimer > vomitCooldown)
        {
            anim.Play("Vomit");
            Instantiate(vomitGO, vomitPos.position, vomitPos.rotation);
            vomitTimer = 0f;
        }
    }


}



