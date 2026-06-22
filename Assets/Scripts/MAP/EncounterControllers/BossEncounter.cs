using UnityEngine;

public class BossEncounter : EncounterController
{
    public HealthStats bossHealth;


    protected override void Start()
    {
        base.Start();    
    }

    protected override void Update()
    {
        base.Update();
        if (inCooldown)
        {
            bossHealth.canTakeDamage = true;
        }
        else
        {
            bossHealth.canTakeDamage = false;
        }
    }

    protected override void WaveCompleted()
    {
        if (bossHealth.health <= 0)
        {
            EncounterCompleted();
        }
        else
        {
            StartWave();
        }
    }

    protected override void StartWave()
    {
        bossHealth.canTakeDamage = false;

        base.StartWave();
    }

    protected override void EncounterCompleted()
    {
        Debug.Log("Boss Defeated");

        arenaController.BossDefeated();

        Destroy(gameObject);
    }
}
