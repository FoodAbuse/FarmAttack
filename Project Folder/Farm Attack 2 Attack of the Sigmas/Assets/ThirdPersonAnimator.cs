using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ThirdPersonAnimator : MonoBehaviour
{
    public Animator ThirdPersonAnimatorr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float moveMagnitude = new Vector2(horizontal, vertical).magnitude;

        ThirdPersonAnimatorr.SetBool("Walking", moveMagnitude > 0);
    }
}
