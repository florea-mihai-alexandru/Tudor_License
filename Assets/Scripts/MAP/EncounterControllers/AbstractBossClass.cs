using UnityEngine;

public abstract class AbstractBossEncounter : EncounterController
{
    public Animator animator;

    protected override void Start()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (ObjectiveCompleted())
            return;

        SetObjectiveDamageable(inCooldown);

        animator.SetBool("IsStunned", inCooldown);
    }

    protected override void StartWave()
    {
        SetObjectiveDamageable(false);

        animator.SetTrigger("Summon");

        base.StartWave();
    }

    protected override void WaveCompleted()
    {
        if (ObjectiveCompleted())
        {
            EncounterCompleted();
            return;
        }

        StartWave();
    }

    protected override void EncounterCompleted()
    {
        animator.SetTrigger("Death");

        Debug.Log("Boss Defeated");

        arenaController.BossDefeated();

        Destroy(gameObject, 2f);
    }

    protected abstract void SetObjectiveDamageable(bool value);

    protected abstract bool ObjectiveCompleted();
}