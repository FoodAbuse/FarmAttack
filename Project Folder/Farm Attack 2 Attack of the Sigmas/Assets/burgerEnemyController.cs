using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class burgerEnemyController : MonoBehaviour
{
    public float ThrowForce = 15f;
    public float throwCooldown = 2f;

    public Animator anim;

    private float timer;

    NavMeshAgent myAgent;
    Rigidbody myRB;

    float playerDistance;
    public Transform player; // Reference to the player's Transform


    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();
        timer = 2;
    }

    void Update()
    {
        if (myAgent.isActiveAndEnabled) // just to stop spamming the stuff about not having one
        {
            myAgent.SetDestination(player.position);

        }

        playerDistance = Vector3.Distance(transform.position, player.position);

        if (playerDistance > 7.5f)
        {
            transform.LookAt(player);
            timer += Time.deltaTime;
            if (timer >= throwCooldown)
            {

                myAgent.enabled = false;
                myRB.isKinematic = false;
                myRB.AddForce(transform.forward * ThrowForce * Time.deltaTime);
                timer = 0;
            }
        }
        if (playerDistance < 7.5f && myRB.velocity == Vector3.zero)
        {
            myAgent.enabled = true;
            myRB.isKinematic = true;
        }

    }

}
