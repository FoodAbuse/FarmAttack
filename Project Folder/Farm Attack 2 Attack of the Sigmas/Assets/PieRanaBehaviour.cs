using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieRanaBehaviour : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float orbitSpeed = 2f;
    public float hoverHeight = 3f;
    public float hoverBobSpeed = 2f;
    public float hoverBobAmount = 0.5f;
    public float changeDirInterval = 5f;

    [Header("Combat")]
    public float preferredDistance = 8f;   // where it likes to orbit
    public float maxChaseDistance = 20f;   // if player gets further, chase mode
    public float vomitRange = 12f;
    public float attackCooldown = 2f;

    [Header("Attack")]
    public GameObject jamProjectile;
    public Transform mouthPoint;
    public float vomitForce = 10f;

    private float cooldownTimer = 0f;
    private float dirTimer = 0f;
    private int orbitDir = 1; // 1 = clockwise, -1 = counterclockwise

    void Start()
    {
        // Randomize initial orbit direction
        orbitDir = (Random.value > 0.5f) ? 1 : -1;
    }

    void Update()
    {
        if (!player) return;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float dist = Vector3.Distance(transform.position, player.position);

        // --- Movement ---
        Vector3 moveDir = Vector3.zero;

        if (dist > maxChaseDistance)
        {
            // Chase mode: just move straight toward player
            moveDir = dirToPlayer;
        }
        else
        {
            // Stay in orbit mode
            if (dist > preferredDistance + 1f)
                moveDir += dirToPlayer; // too far → move closer
            else if (dist < preferredDistance - 1f)
                moveDir -= dirToPlayer; // too close → back off

            // Orbiting movement
            Vector3 sideDir = Quaternion.Euler(0, orbitDir * 90, 0) * dirToPlayer;
            moveDir += sideDir * orbitSpeed;

            // Add some noise so it’s not perfect
            moveDir += new Vector3(Mathf.PerlinNoise(Time.time, 0) - 0.5f, 0, Mathf.PerlinNoise(0, Time.time) - 0.5f);

            // Occasionally flip orbit direction
            dirTimer += Time.deltaTime;
            if (dirTimer >= changeDirInterval)
            {
                if (Random.value > 0.5f) orbitDir *= -1;
                dirTimer = 0f;
            }
        }

        // Apply movement with hover bobbing
        float bob = Mathf.Sin(Time.time * hoverBobSpeed) * hoverBobAmount;
        Vector3 targetPos = transform.position + (moveDir.normalized * moveSpeed * Time.deltaTime);
        targetPos.y = player.position.y + hoverHeight + bob;
        transform.position = targetPos;

        // Always face player
        Vector3 lookPos = player.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        // --- Combat ---
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

        if (dist <= vomitRange && cooldownTimer <= 0)
            Vomit();
    }

    void Vomit()
    {
        Debug.Log("Pie-ranha vomits jam!");

        if (jamProjectile && mouthPoint)
        {
            GameObject jam = Instantiate(jamProjectile, mouthPoint.position, transform.rotation);
            Rigidbody rb = jam.GetComponent<Rigidbody>();
            if (rb) rb.AddForce(transform.forward * vomitForce, ForceMode.Impulse);
        }

        cooldownTimer = attackCooldown;
    }
}
