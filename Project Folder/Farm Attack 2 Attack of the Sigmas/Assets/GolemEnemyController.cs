using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GolemEnemyController : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public Transform throwPoint;
    public GameObject boulderPrefab;
    public float throwCooldown = 3f;
    float timer;

    void Update()
    {
        if (!player) return;

        agent.SetDestination(player.position);

        timer -= Time.deltaTime;
        if (timer <= 0f && Vector3.Distance(transform.position, player.position) < 15f)
        {
            Throw();
            timer = throwCooldown;
        }
    }

    void Throw()
    {
        GameObject b = Instantiate(boulderPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = b.GetComponent<Rigidbody>();
        rb.velocity = (player.position - throwPoint.position).normalized * 10f + Vector3.up * 5f;
    }
}
