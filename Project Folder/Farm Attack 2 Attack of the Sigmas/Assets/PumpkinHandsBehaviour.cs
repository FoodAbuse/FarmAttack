using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinHandsBehaviour : MonoBehaviour
{
    public Camera cam;             
    public GameObject prefab;       
    public Transform preview;       

    public LayerMask floorMask;      
    public float maxDistance = 100f;

    public Animator anim;

    public RaycastHit hit;
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, maxDistance, floorMask))
        {
            preview.position = hit.point;

            Vector3 forward = cam.transform.forward;
            forward.y = 0f;
            if (forward.sqrMagnitude > 0.001f)
                preview.rotation = Quaternion.LookRotation(forward);

            if (Input.GetMouseButtonDown(1))
            {
                anim.SetBool("Place", true);
            }
        }
    }

    public void Place()
    {
        Instantiate(prefab, hit.point, preview.rotation);
    }

    public void EndPlace()
    {
        anim.SetBool("Place", false);

    }

}
