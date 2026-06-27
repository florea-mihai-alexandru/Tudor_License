using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public enum BossState
    {
        Stunned,
        SpawningEnemies,
        WaitingForEnemies,
        Dying
    }

    public BossState CurrentState { get; private set; }

    [Header("Enemy Spawn")]
    public Enemy enemyPrefab;
    public Transform[] spawnPoints;

    [Header("Boss Settings")]
    public int enemiesPerWave = 3;
    public float stunDuration = 5f;

    private List<Enemy> aliveEnemies = new List<Enemy>();

    private float stunTimer;

    public Transform playerTransform;

    public HealthStats healthStats;
    public float deathDuratoion = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthStats = GetComponentInChildren<HealthStats>();
        healthStats.canTakeDamage = false;
        EnterStunnedState();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthStats.health <= 0)
        {
            CurrentState = BossState.Dying;
        }
        Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case BossState.WaitingForEnemies:
                UpdateCheckEnemies();
                break;

            case BossState.Stunned:
                UpdateStunned();
                break;
            case BossState.Dying:
                UpdateDying();
                break;
        }
                

    }

    void StartEnemyPhase()
    {
        Debug.Log("Spawning");
        CurrentState = BossState.SpawningEnemies;
        healthStats.canTakeDamage = false;

        aliveEnemies.Clear();

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Enemy enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
           
            enemy.PlayerTransform = playerTransform;
            aliveEnemies.Add(enemy);
        }

        CurrentState = BossState.WaitingForEnemies;
    }

    void UpdateCheckEnemies()
    {
        aliveEnemies.RemoveAll(enemy => enemy == null);

        if (aliveEnemies.Count == 0)
        {
            Debug.Log("Killed all enemies");
            EnterStunnedState();
        }
    }

    void EnterStunnedState()
    {
        CurrentState = BossState.Stunned;

        stunTimer = stunDuration;

        healthStats.canTakeDamage = true;

        Debug.Log("Boss is stunned!");
    }

    void UpdateStunned()
    {
        stunTimer -= Time.deltaTime;
        if (stunTimer <= 0)
        {
            StartEnemyPhase();
        }
    }
    void UpdateDying()
    {
        deathDuratoion-= Time.deltaTime;
        if (deathDuratoion <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Die()
    {
        Debug.Log("Boss Defeated!");

        Destroy(gameObject);
    }
}
