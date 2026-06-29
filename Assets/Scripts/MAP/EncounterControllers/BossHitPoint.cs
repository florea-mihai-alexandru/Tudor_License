using UnityEngine;

public class BossHitPoint : MonoBehaviour
{
    public HealthStats health;

    public bool IsDestroyed => health.health <= 0;

    public void SetCanTakeDamage(bool value)
    {
        health.canTakeDamage = value;
    }
}