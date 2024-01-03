using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBehaviour : MonoBehaviour
{
    Animator myAnim;


    // Temp - WIP Shotgun Testing
    ShotgunCorn shotgunBhvr;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();

        // Temp - WIP Shotgun Testing
        shotgunBhvr = GetComponentInChildren<ShotgunCorn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myAnim.Play("Shoot");
            Camera.main.GetComponent<cameraShakeBehaviour>().ShakeCamera();
        }
        if (Input.GetMouseButtonUp(0))
        {
            myAnim.Play("Idle");
        }
    }


    // Temp - WIP Shotgun Testing
    // deployed by animation event
    public void Shoot()
    {
        if(shotgunBhvr != null)
        {
            shotgunBhvr.Fire();
        }
    }
}
