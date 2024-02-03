using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gumballEnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform shootPosition;
    public Transform Body;

    public GameObject GumballProjectile;

    public float rotationSpeed = 5.0f; // Adjust the speed as needed
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = new Vector3(player.position.x, Body.position.y, player.position.z);

        // Smoothly rotate towards the player with the specified rotation speed
        Quaternion targetRotation = Quaternion.LookRotation(playerPosition - Body.position);
        Body.rotation = Quaternion.Slerp(Body.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer > 0.75f)
        {
            GameObject newGumball = Instantiate(GumballProjectile, shootPosition.position, shootPosition.rotation);
            newGumball.GetComponent<Rigidbody>().AddForce(newGumball.transform.forward * 2500);
            Destroy(newGumball, 5);
            timer = 0;
        }
    }
}
