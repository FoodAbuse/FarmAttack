using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanBehaviour : MonoBehaviour
{
    public float maxWater = 10f;          
    public float currentWater;             
    public float waterRate = 5f;         

    public float sprayRange = 5f;        

    void Start()
    {
        currentWater = maxWater;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && currentWater > 0f)
            SprayWater();
    }

    void SprayWater()
    {
        float delta = waterRate * Time.deltaTime;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, sprayRange))
        {
            var plant = hit.collider.GetComponent<CropPrefabBehaviour>();
            if (plant != null)
            {
                plant.WaterPlant(delta);
                currentWater = Mathf.Max(0f, currentWater - delta);
            }
        }
    }

   
    public void Refill()
    {
        currentWater = maxWater;
    } 
}
