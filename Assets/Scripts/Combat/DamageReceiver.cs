using UnityEngine;

public class DamageReceiver : MonoBehaviour, IDamageable
{
    private HealthStats stats;

    public void Damage(float amount)
    {
        Debug.Log(gameObject.name + " DamageWFBL;Hwljbd!");
        stats?.TakeDamage(amount);
    }

    private void Awake()
    {
        stats = GetComponent<HealthStats>();
    }
}