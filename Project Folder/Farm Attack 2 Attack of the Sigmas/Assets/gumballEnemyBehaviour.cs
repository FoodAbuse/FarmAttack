using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gumballEnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform shootPosition;
    public Transform Body;

    public GameObject GumballProjectile;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  
    }

    // Update is called once per frame
    void Update()
    {
        Body.LookAt(player);

        timer += Time.deltaTime;
        if(timer > .75f)
        {
            GameObject newGumball = Instantiate(GumballProjectile, shootPosition.position, shootPosition.rotation);
            newGumball.GetComponent<Rigidbody>().AddForce(newGumball.transform.forward * 2500);
            Destroy(newGumball, 5);
            timer = 0;
        }

    }
}
