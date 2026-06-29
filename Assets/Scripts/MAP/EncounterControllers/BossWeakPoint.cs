using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    public HealthStats health;

    public System.Action<BossWeakPoint> OnDestroyed;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        health.OnDeath += HandleDeath;
    }

    public void SetDamageable(bool value)
    {
        health.canTakeDamage = value;

        animator.SetBool("Stunned", value);
    }

    private void HandleDeath()
    {
        OnDestroyed?.Invoke(this);

        Destroy(gameObject);
    }
}