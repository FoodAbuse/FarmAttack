using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceCreamBehaviour : MonoBehaviour
{
   
    public GameObject bulletPrefab;

    public Transform gun1;
    public Transform gun2;

    public Animator gun1Anim;
    public Animator gun2Anim;

    public int ThrowForce;

    public NavMeshAgent myAgent;
    public Transform player;

    private void Start()
    {
        // Start the shooting coroutine when the script starts
        StartCoroutine(ShootRoutine());
    }


    private void Update()
    {
        myAgent.SetDestination(player.position);

    }

    private System.Collections.IEnumerator ShootRoutine()
    {
        while (true)
        {
            // Wait for 2.5 seconds
            yield return new WaitForSeconds(2.5f);
            gun1Anim.Play("Shoot");
            GameObject projectile1 = Instantiate(bulletPrefab, gun1.position, gun1.rotation);


            Rigidbody rb1 = projectile1.GetComponent<Rigidbody>();

            rb1.AddForce(rb1.transform.forward * ThrowForce, ForceMode.Impulse);

            // Wait for another 2.5 seconds
            yield return new WaitForSeconds(2.5f);
            gun2Anim.Play("Shoot");

            GameObject projectile2 = Instantiate(bulletPrefab, gun2.position, gun1.rotation);


            Rigidbody rb2 = projectile2.GetComponent<Rigidbody>();

            rb2.AddForce(rb2.transform.forward * ThrowForce, ForceMode.Impulse);
        }
    }

    
}
