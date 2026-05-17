using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public enum BossState
    {
        Stunned,
        SpawningEnemies,
        WaitingForEnemies
    }

    public BossState CurrentState { get; private set; }

    [Header("Enemy Spawn")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    [Header("Boss Settings")]
    public int enemiesPerWave = 3;
    public float stunDuration = 5f;

    [Header("Boss HP")]
    public int maxHealth = 100;
    private int currentHealth;

    private List<GameObject> aliveEnemies = new List<GameObject>();

    private float stunTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        EnterStunnedState();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case BossState.WaitingForEnemies:
                UpdateCheckEnemies();
                break;

            case BossState.Stunned:
                UpdateStunned();
                break;
        }
                

    }

    void StartEnemyPhase()
    {
        Debug.Log("Spawning");
        CurrentState = BossState.SpawningEnemies;

        aliveEnemies.Clear();

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

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

        Debug.Log("Boss is stunned!");
    }

    void UpdateStunned()
    {
        stunTimer -= Time.deltaTime;
        //Debug.Log(stunTimer.ToString() + "timer");
        if (stunTimer <= 0)
        {
            //if (currentHealth <= maxHealth * 0.3f)
            //{
            //    EnterEnragedState();
            //}
            //else
            //{
            //    StartEnemyPhase();
            //}
            StartEnemyPhase();
        }
    }

    public void TakeDamage(int damage)
    {
        if (CurrentState != BossState.Stunned)
            return;

        currentHealth -= damage;

        Debug.Log("Boss HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); //DIE
        }
    }

    void Die()
    {
        Debug.Log("Boss Defeated!");

        Destroy(gameObject);
    }
}
