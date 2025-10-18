using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeOnImpact : MonoBehaviour
{
    public GameObject myExplosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "World")
        {

            GameObject newExplosion = Instantiate(myExplosion, transform.position, transform.rotation);
            Destroy(newExplosion, 1);
            Destroy(gameObject);
        }
    }
}
