using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackBehaviour : MonoBehaviour
{
    public int myDamageAmount;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SendDamage()
    {

        float spreadAngle = 10f; // degrees
        float maxDistance = 10f;
        float yOffset = 1f; // raise the ray origin by 1 unit
        int layerMask = ~0; // all layers

        Vector3 origin = transform.position + Vector3.up * yOffset;

        Vector3[] directions = {
        transform.forward,
        Quaternion.Euler(0, -spreadAngle, 0) * transform.forward,
        Quaternion.Euler(0, spreadAngle, 0) * transform.forward
    };

        foreach (var dir in directions)
        {
            Debug.DrawRay(origin, dir * maxDistance, Color.red);

            if (Physics.Raycast(origin, dir, out RaycastHit hit, maxDistance, layerMask))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    FindObjectOfType<playerHealthBehaviour>().TakeDamage(myDamageAmount);
                    break;
                }

                Debug.Log("Hit: " + hit.transform.name);
            }
        }
    }
}

