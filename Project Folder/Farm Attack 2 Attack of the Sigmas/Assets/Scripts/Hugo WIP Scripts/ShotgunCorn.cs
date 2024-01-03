using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunCorn : MonoBehaviour
{
    [Header("Firing")]
    [Space(10)]
    [SerializeField]
    private bool isSelectedAmmo;
    [Space(3)]
    [SerializeField]
    private bool canFire;
    [Space(6)]
    [SerializeField]
    private float fireRate;
    [Space(25)]

    [Header("Static Variables")]
    [SerializeField]
    private Transform projectileSpawn;



    // Start is called before the first frame update
    void Start()
    {
        if(projectileSpawn == null)
        {
            projectileSpawn = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (canFire == true)
        {
            Debug.Log("firing");
            ParticleSystem p;
            p = GetComponentInChildren<ParticleSystem>();
            p.Play();
        }
    }
}
