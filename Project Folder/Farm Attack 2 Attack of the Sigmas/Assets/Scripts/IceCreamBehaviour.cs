using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceCreamBehaviour : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform gun1;
    public Transform gun2;
    public int ThrowForce;

    public NavMeshAgent myAgent;
    public Transform player;

    public Animator anim;
    public Transform torsoPivot; // The pivot for torso rotation

    public GameObject explodeGO;
    public int health;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ShootRoutine());
    }

    private void Update()
    {
        HandleMovement();
        CheckHealth();
    }

    private void LateUpdate()
    {
        LookAtPlayerWithRestrictions();
    }

    private void HandleMovement()
    {
        if (!myAgent.enabled) return;

        myAgent.SetDestination(player.position);
    }

    private void CheckHealth()
    {
        if (health > 0) return;

        Instantiate(explodeGO, transform.position, transform.rotation);
        Destroy(explodeGO, 5);
        Destroy(gameObject);
    }

    private void LookAtPlayerWithRestrictions()
    {
        Vector3 directionToPlayer = player.position - torsoPivot.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        Vector3 euler = lookRotation.eulerAngles;

        // Calculate the desired rotation in local space
        Quaternion localRotation = Quaternion.Inverse(transform.rotation) * Quaternion.Euler(euler);
        Vector3 localEuler = localRotation.eulerAngles;

        // Normalize and clamp the angles
        localEuler.x = NormalizeAngle(localEuler.x);
        localEuler.y = NormalizeAngle(localEuler.y);
        localEuler.x = Mathf.Clamp(localEuler.x, -25f, 25f);
        localEuler.y = Mathf.Clamp(localEuler.y, -45f, 45f);

        // Apply the clamped rotation back in world space
        Quaternion clampedLocalRotation = Quaternion.Euler(localEuler);
        torsoPivot.rotation = transform.rotation * clampedLocalRotation;
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.25f);
            ShootFromGun(gun1);

            yield return new WaitForSeconds(1.25f);
            ShootFromGun(gun2);
        }
    }

    private void ShootFromGun(Transform gun)
    {
        GameObject projectile = Instantiate(bulletPrefab, gun.position, gun.rotation);
        projectile.transform.LookAt(player);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * ThrowForce, ForceMode.Impulse);
    }

    public void TakeDamage()
    {
        health -= 1;
    }
}
