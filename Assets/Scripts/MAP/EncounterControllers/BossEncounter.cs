using UnityEngine;

public class BossEncounter : AbstractBossEncounter
{
    public HealthStats bossHealth;

    protected override void SetObjectiveDamageable(bool value)
    {
        bossHealth.canTakeDamage = value;
    }

    protected override bool ObjectiveCompleted()
    {
        return bossHealth.health <= 0;
    }
}