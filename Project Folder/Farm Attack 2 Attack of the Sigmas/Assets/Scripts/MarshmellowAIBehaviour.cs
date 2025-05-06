using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarshmellowAIBehaviour : MonoBehaviour
{

    [Header("Vomit Settings")]
    public GameObject vomitGO;
    public Transform player;
    public Transform vomitPos;
    public int minVomitDistance = 5;
    public int maxVomitDistance = 10;
    public float vomitCooldown = 2f;

    [Header("Hop Movement Settings")]
    public float moveDuration = 1f;      // How long it moves
    public float stopDuration = 0.25f;   // How long it pauses

    private float vomitTimer;
    private float movementTimer;
    private bool isStopped = false;

    private NavMeshAgent myAgent;
    private float distanceFromPlayer;

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
            Instantiate(vomitGO, vomitPos.position, vomitPos.rotation);
            vomitTimer = 0f;
        }
    }
}



