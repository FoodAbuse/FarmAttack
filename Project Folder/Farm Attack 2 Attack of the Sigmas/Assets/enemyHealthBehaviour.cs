using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthBehaviour : MonoBehaviour
{
    public float myHealth;
    public float damageAmount;

    public GameObject explodeGO;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(myHealth <= 0)
        {
            Instantiate(explodeGO, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        myHealth += -damageAmount;
    }
}
