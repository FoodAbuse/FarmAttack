using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShopBehaviour : MonoBehaviour
{
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCam == null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();

        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5))

        {
            if (hit.transform.tag == "Shop")
            {
                hit.transform.SendMessage("Hit");
            }
        }
    }
}
