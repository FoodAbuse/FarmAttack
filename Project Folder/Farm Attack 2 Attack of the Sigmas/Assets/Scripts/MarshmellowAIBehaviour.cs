using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarshmellowAIBehaviour : MonoBehaviour
{
    public GameObject vomitGO;

    public Transform player;
    public Transform vomitPos;
    public int minVomitDistance;
    public int maxVomitDistance;
    float timeBetweenVomit;
    NavMeshAgent myAgent;

    float distanceFromPlayer;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        myAgent.SetDestination(player.position);
        timeBetweenVomit += Time.deltaTime;
        if(distanceFromPlayer >= minVomitDistance && distanceFromPlayer <= maxVomitDistance && timeBetweenVomit > 2)
        {

            Instantiate(vomitGO, vomitPos.position, vomitPos.rotation);
            timeBetweenVomit = 0;
        }
    }

 
}
