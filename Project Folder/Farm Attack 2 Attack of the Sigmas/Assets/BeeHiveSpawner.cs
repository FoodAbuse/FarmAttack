using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHiveSpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject beeHivePrefab;
    [Range(0f, 1f)] public float chancePerTree = 0.05f;
    public float yOffset = 1.5f;

    void Start()
    {
        if (!terrain) terrain = FindObjectOfType<Terrain>();
        var td = terrain.terrainData;

        foreach (var t in td.treeInstances)
        {
            if (Random.value > chancePerTree) continue;

            // convert tree position (normalized 0–1) into world space
            Vector3 worldPos = Vector3.Scale(t.position, td.size) + terrain.transform.position;
            // drop to ground
            worldPos.y = terrain.SampleHeight(worldPos) + terrain.transform.position.y;

            Instantiate(beeHivePrefab, worldPos + Vector3.up * yOffset, Quaternion.identity);
        }
    }
}
