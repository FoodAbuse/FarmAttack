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
        FindObjectOfType<playerHealthBehaviour>().TakeDamage(myDamageAmount);
    }


}
