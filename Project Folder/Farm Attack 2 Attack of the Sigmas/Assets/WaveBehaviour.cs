using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[System.Serializable]
public class EnemySpawnInfo
{
    public NetworkObject enemyPrefab;
    public int count = 1;
    public float spawnDelay = 0.2f;
}

[System.Serializable]
public class Wave
{
    public List<EnemySpawnInfo> spawnGroups = new ();
}

public class WaveBehaviour : NetworkBehaviour
{
    public List<Wave> waves = new ();
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 10f;
    public bool autoStart = true;

    int currentWaveIndex = 0;
    bool isSpawning = false;
    bool hasStarted = false;

    public delegate void WaveEvent(int waveNumber);
    public event WaveEvent OnWaveStarted;
    public event WaveEvent OnWaveEnded;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        if (autoStart && !hasStarted) BeginWaves();
    }

    public void BeginWaves()
    {
        if (!IsServer) return;
        if (hasStarted) return;
        hasStarted = true;
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        if (currentWaveIndex >= waves.Count)
            yield break;

        yield return SpawnWaveCoroutine(currentWaveIndex, 0f);
        currentWaveIndex++;

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

        OnWaveEnded?.Invoke(currentWaveIndex);
        StartCoroutine(StartNextWave());
    }

    public void SpawnWave(int waveIndex, float percentToPlayer)
    {
        if (!IsServer) return;
        if (waveIndex < 0 || waveIndex >= waves.Count) return;
        if (!isSpawning) StartCoroutine(SpawnWaveCoroutine(waveIndex, percentToPlayer));
    }

    IEnumerator SpawnWaveCoroutine(int waveIndex, float percentToPlayer)
    {
        if (!IsServer) yield break;

        var wave = waves[waveIndex];
        isSpawning = true;
        OnWaveStarted?.Invoke(waveIndex + 1);

        foreach (var group in wave.spawnGroups)
            yield return StartCoroutine(SpawnGroup(group, percentToPlayer));

        isSpawning = false;
    }

    IEnumerator SpawnGroup(EnemySpawnInfo group, float percentToPlayer)
    {
        if (!IsServer) yield break;

        int total = Mathf.Max(0, group.count);
        int toPlayer = Mathf.RoundToInt(total * percentToPlayer);
        int sentToPlayer = 0;

        for (int i = 0; i < total; i++)
        {
            var sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var enemy = Instantiate(group.enemyPrefab, sp.position, Quaternion.identity);

            var ai = enemy.GetComponent<burgerEnemyController>();
            if (ai != null)
            {
                var intent = (sentToPlayer < toPlayer)
                    ? burgerEnemyController.TargetIntent.Player
                    : burgerEnemyController.TargetIntent.Planter;

                // If ai.intent is a NetworkVariable<TargetIntent>, use: ai.intent.Value = intent;
                ai.intent = intent;

                if (sentToPlayer < toPlayer) sentToPlayer++;
            }

            enemy.Spawn();

            yield return new WaitForSeconds(group.spawnDelay);
        }
    }
}