using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBehaviour : MonoBehaviour
{
    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            myAnim.Play("Shoot");
            Camera.main.GetComponent<cameraShakeBehaviour>().ShakeCamera();
        }
        if (Input.GetMouseButtonUp(0))
        {
            myAnim.Play("Idle");
        }
    }
}
