using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBehaviour : MonoBehaviour
{

    // THIS SCRIPT FUCKING SUCKS IT DOUBLES THE DAMAGE SO HALF THE AMOUNT (WANTING 2 DAMAGE, PUT INPUT THE DAMAGE AS 1)

    public float deathTimerAmount;
    public float DamageAmount;
    public float timer;

    void Start()
    {
      
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= .1f)
        {
            GetComponent<BoxCollider>().enabled = false;

        }
        if(timer > deathTimerAmount)
        {
            Destroy(gameObject);
        }

    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHealthBehaviour health = collision.gameObject.GetComponent<enemyHealthBehaviour>();
            health.TakeDamage(DamageAmount);

        }
    }
}
