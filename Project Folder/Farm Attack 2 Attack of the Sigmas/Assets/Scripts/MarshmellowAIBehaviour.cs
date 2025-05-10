using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarshmellowAIBehaviour : MonoBehaviour
{

    public GameObject vomitGO;
    public Transform player;
    public Transform vomitPos;
    public int minVomitDistance = 5;
    public int maxVomitDistance = 10;
    public float vomitCooldown = 2f;

    public float moveDuration;      // How long it moves
    public float stopDuration;   // How long it pauses

    float vomitTimer;
    float movementTimer;
    bool isStopped = false;

    NavMeshAgent myAgent;
    float distanceFromPlayer;

    public Animator anim;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Update movement state
        movementTimer += Time.deltaTime;

        if (!isStopped && movementTimer >= moveDuration)
        {
            myAgent.isStopped = true;
            isStopped = true;
            movementTimer = 0f;
        }
        else if (isStopped && movementTimer >= stopDuration)
        {
            myAgent.isStopped = false;
            isStopped = false;
            movementTimer = 0f;
        }

        // Move toward player only while not stopped
        if (!isStopped)
        {
            myAgent.SetDestination(player.position);
        }

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



