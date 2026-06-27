using System.Collections.Generic;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public Transform[] spawnPoints;

    public int totalWaves = 3;
    public int enemiesPerWave = 3;

    public float waveCooldown = 3f;
    private float remainingCooldown = 3f;
    protected bool inCooldown = false;

    protected int currentWave;

    protected List<Enemy> aliveEnemies =
        new List<Enemy>();

    protected ArenaController arenaController;

    protected virtual void Start()
    {
        StartWave();
        arenaController = GetComponentInParent<ArenaController>();
    }

    protected virtual void Update()
    {
        aliveEnemies.RemoveAll(e => e == null);

        if (aliveEnemies.Count == 0)
        {
            if(remainingCooldown > 0f)
            {
                remainingCooldown -= Time.deltaTime;
                inCooldown = true;
            }
            else
            { 
                WaveCompleted(); 
                inCooldown = false;
                remainingCooldown = waveCooldown;
            }
        }
    }

    protected virtual void StartWave()
    {
        aliveEnemies.Clear();

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawn =
                spawnPoints[Random.Range(0, spawnPoints.Length)];

            Enemy enemyPrefab =
                enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Enemy enemy =
                Instantiate(enemyPrefab,
                    spawn.position,
                    Quaternion.identity);

            aliveEnemies.Add(enemy);
        }
    }

    protected virtual void WaveCompleted()
    {
        currentWave++;

        if (currentWave >= totalWaves)
        {
            EncounterCompleted();
            return;
        }

        StartWave();
    }

    protected virtual void EncounterCompleted()
    {
        Debug.Log("Encounter Complete");

    }
}