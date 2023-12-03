using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class burgerEnemyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform throwPoint;
    public float ThrowForce = 15f;
    public float throwCooldown = 2f;

    public Animator anim;

    private float lastThrowTime;

    public Transform player; // Reference to the player's Transform

    NavMeshAgent myAgent;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Time.time - lastThrowTime >= throwCooldown)
        {
         
            ThrowProjectile();

       
            lastThrowTime = Time.time;
        }
        else
        {
            anim.SetBool("Spit", false);
        }
        myAgent.SetDestination(player.position);
    }

    void FixedUpdate()
    {
       
    }
    void ThrowProjectile()
    {
        anim.SetBool("Spit", true);
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);


        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.AddForce(rb.transform.forward * ThrowForce, ForceMode.Impulse);
    }
}
