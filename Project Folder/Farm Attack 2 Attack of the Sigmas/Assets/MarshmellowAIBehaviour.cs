using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarshmellowAIBehaviour : MonoBehaviour
{
    public Transform player;

    NavMeshAgent myAgent;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        myAgent.SetDestination(player.position);
    }

    void FixedUpdate()
    {

    }
}
