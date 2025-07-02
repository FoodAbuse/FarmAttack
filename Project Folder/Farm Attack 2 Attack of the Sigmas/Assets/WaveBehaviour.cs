using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public int count;
    public float spawnDelay; // Delay between each enemy in this group
}

[System.Serializable]
public class Wave
{
    public List<EnemySpawnInfo> spawnGroups;
}

public class WaveBehaviour : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<Wave> waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 10f;
    public bool autoStart = true;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;
    private bool hasStarted = false;

    public delegate void WaveEvent(int waveNumber);
    public event WaveEvent OnWaveStarted;
    public event WaveEvent OnWaveEnded;

    void Start()
    {
        if (autoStart)
        {
            BeginWaves();
        }
    }

    public void BeginWaves()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            StartCoroutine(StartNextWave());
        }
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("All waves complete!");
            yield break;
        }

        Wave wave = waves[currentWaveIndex];
        isSpawning = true;
        OnWaveStarted?.Invoke(currentWaveIndex + 1);

        foreach (var group in wave.spawnGroups)
        {
            yield return StartCoroutine(SpawnGroup(group));
        }

        isSpawning = false;
        currentWaveIndex++;

        // Wait until all enemies are cleared
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

        OnWaveEnded?.Invoke(currentWaveIndex);
        StartCoroutine(StartNextWave());
    }

    IEnumerator SpawnGroup(EnemySpawnInfo group)
    {
        for (int i = 0; i < group.count; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(group.enemyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(group.spawnDelay);
        }
    }
}
