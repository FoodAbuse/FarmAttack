using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >=3)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.transform.position, bulletSpawnPos.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            Destroy(bullet);
            timer = 0;
        }
    }
}
