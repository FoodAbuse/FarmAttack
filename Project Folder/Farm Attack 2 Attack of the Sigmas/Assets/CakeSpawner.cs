using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSpawner : MonoBehaviour
{
    public GameObject Cake;

    public int numberToSpawn = 3;

    public Transform[] spawnAreas; // Each one defines a center and area size (via its scale)

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Transform area = spawnAreas[Random.Range(0, spawnAreas.Length)];

            Vector3 spawnPos = GetRandomPointInArea(area);
            Instantiate(Cake, spawnPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomPointInArea(Transform area)
    {
        Vector3 halfExtents = area.localScale / 2f;

        float x = Random.Range(-halfExtents.x, halfExtents.x);
        float y = Random.Range(-halfExtents.y, halfExtents.y);
        float z = Random.Range(-halfExtents.z, halfExtents.z);

        return area.position + area.rotation * new Vector3(x, y, z); // rotates with area
    }

    
}
