using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoFlyToPlayer : MonoBehaviour
{
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        transform.Translate(Vector3.forward * 15 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            // add ammo here, systems not in yet!
        }
    }
}
