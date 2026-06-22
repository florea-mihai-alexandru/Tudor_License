using System.Collections.Generic;
using UnityEngine;

public class LevelStateMachine : MonoBehaviour
{
    public enum LevelState
    {
        Stunned,
        SpawningEnemies,
        WaitingForEnemies,
        Dying
    }

    public LevelState CurrentState { get; private set; }

    [Header("Enemy Spawn")]
    public Enemy enemyPrefab;
    public Transform[] spawnPoints;

    [Header("Level Settings")]
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
            CurrentState = LevelState.Dying;
        }
        Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case LevelState.WaitingForEnemies:
                UpdateCheckEnemies();
                break;

            case LevelState.Stunned:
                UpdateStunned();
                break;
            case LevelState.Dying:
                UpdateDying();
                break;
        }
                

    }

    void StartEnemyPhase()
    {
        Debug.Log("Spawning");
        CurrentState = LevelState.SpawningEnemies;
        healthStats.canTakeDamage = false;

        aliveEnemies.Clear();

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Enemy enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
           
            enemy.PlayerTransform = playerTransform;
            aliveEnemies.Add(enemy);
        }

        CurrentState = LevelState.WaitingForEnemies;
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
        CurrentState = LevelState.Stunned;

        stunTimer = stunDuration;

        healthStats.canTakeDamage = true;

        Debug.Log("Level is stunned!");
    }

    void UpdateStunned()
    {
        stunTimer -= Time.deltaTime;
        if (stunTimer <= 0)
        {
            healthStats.TakeDamage(1);
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
}
