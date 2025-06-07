using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropPrefabBehaviour : MonoBehaviour
{
    public int myGrowthTime;

    public GameObject[] myChildren;

    public GameObject PublicAmmoGO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HasBeenHarvested()
    {
        foreach (GameObject child in myChildren)
        {
            Instantiate(PublicAmmoGO, child.transform.position, PublicAmmoGO.transform.rotation);
        }


    }
}
