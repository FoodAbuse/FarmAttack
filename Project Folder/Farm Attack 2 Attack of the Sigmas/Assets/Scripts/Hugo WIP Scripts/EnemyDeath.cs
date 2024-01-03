using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    private int hitCount;

    [SerializeField]
    private int currentHealth;

    private int maxHealth = 5;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        hitCount++;

        currentHealth--;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("Enemy Killed");
    }

    private void OnTriggerEnter(Collider c)
    {
        //Debug.Log(c.name);
    }


}
