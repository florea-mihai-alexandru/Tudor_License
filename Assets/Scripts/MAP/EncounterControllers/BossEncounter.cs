using UnityEngine;

public class BossEncounter : EncounterController
{
    public HealthStats bossHealth;

    private Animator animator;

    protected override void Start()
    {
        animator = GetComponentInChildren<Animator>();

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (bossHealth.health <= 0)
            return;

        if (inCooldown)
        {
            bossHealth.canTakeDamage = true;
            animator.SetBool("IsStunned", true);
        }
        else
        {
            bossHealth.canTakeDamage = false;
            animator.SetBool("IsStunned", false);
        }
    }

    protected override void StartWave()
    {
        bossHealth.canTakeDamage = false;

        // Play summon animation
        animator.SetTrigger("Summon");

        // Spawn enemies immediately
        base.StartWave();
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

    protected override void EncounterCompleted()
    {
        animator.SetTrigger("Death");

        Debug.Log("Boss Defeated");

        arenaController.BossDefeated();

        Destroy(gameObject, 2f);
    }
}