using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    public float speed;
    public float damage;

    Rigidbody rb;

    public GameObject impactPrefab;
    public bool isPotato;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed * 100 * Time.deltaTime);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "World")
        {
            if(!isPotato)
            {
                GameObject explode = Instantiate(impactPrefab, transform.position, transform.rotation);
                Destroy(explode, .25f);
                Destroy(gameObject);

            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isPotato)
            {
                collision.transform.SendMessage("Flinch");
                GameObject explode = Instantiate(impactPrefab, transform.position, transform.rotation);
                Destroy(explode, .25f);
                Destroy(gameObject);

            }
        }

        if (isPotato)
        {

            GameObject explode = Instantiate(impactPrefab, transform.position, transform.rotation);
            Destroy(explode, 1);
            
            Destroy(gameObject);

        }
    }
}
