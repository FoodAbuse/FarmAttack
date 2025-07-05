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

        // By default spawn with 0% targeting player (all crops), you can customize as needed
        yield return SpawnWaveCoroutine(currentWaveIndex, 0f);

        currentWaveIndex++;

        // Wait until all enemies are cleared
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

        OnWaveEnded?.Invoke(currentWaveIndex);
        StartCoroutine(StartNextWave());
    }

    // New method: Spawn a specific wave with percentage of enemies targeting player
    public void SpawnWave(int waveIndex, float percentToPlayer)
    {
        if (waveIndex < 0 || waveIndex >= waves.Count)
        {
            Debug.LogWarning("Wave index out of range!");
            return;
        }

        if (!isSpawning)
        {
            StartCoroutine(SpawnWaveCoroutine(waveIndex, percentToPlayer));
        }
    }

    // Coroutine for spawning waves with targeting intent
    private IEnumerator SpawnWaveCoroutine(int waveIndex, float percentToPlayer)
    {
        Wave wave = waves[waveIndex];
        isSpawning = true;
        OnWaveStarted?.Invoke(waveIndex + 1);

        foreach (var group in wave.spawnGroups)
        {
            yield return StartCoroutine(SpawnGroup(group, percentToPlayer));
        }

        isSpawning = false;
    }

    // Modified SpawnGroup to assign targeting intent to enemies
    IEnumerator SpawnGroup(EnemySpawnInfo group, float percentToPlayer)
    {
        int total = group.count;
        int toPlayer = Mathf.RoundToInt(total * percentToPlayer);
        int spawnedForPlayer = 0;

        for (int i = 0; i < total; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyGO = Instantiate(group.enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Assign targeting intent
            burgerEnemyController enemyAI = enemyGO.GetComponent<burgerEnemyController>();
            if (enemyAI != null)
            {
                if (spawnedForPlayer < toPlayer)
                {
                    enemyAI.intent = burgerEnemyController.TargetIntent.Player;
                    spawnedForPlayer++;
                }
                else
                {
                    enemyAI.intent = burgerEnemyController.TargetIntent.Planter;
                }
            }

            yield return new WaitForSeconds(group.spawnDelay);
        }
    }
}