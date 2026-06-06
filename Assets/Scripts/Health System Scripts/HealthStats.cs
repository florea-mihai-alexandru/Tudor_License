/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class HealthStats : MonoBehaviour, IDamageable
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    [SerializeField]
    private static HealthStats instance;
    public static HealthStats Instance
    {
        get;
    }
    #endregion

    [SerializeField]
    public float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public bool canTakeDamage = true;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    private float remainingCooldown;
    public float damageCooldown = 0f;

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg, float enemyAtDuration = 0)
    {
        if (enemyAtDuration == 0)
        {
            enemyAtDuration = damageCooldown;
        }
        if (canTakeDamage)
        {
            if (remainingCooldown <= 0f)
            { 
                health -= dmg;
                ClampHealth();

                remainingCooldown = enemyAtDuration;
            }
            else
            {
                remainingCooldown -= Time.deltaTime;
            }
        }
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }   
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    public void Damage(float amount)
    {
        Debug.Log("werghjnwgg");
        TakeDamage(amount);

    }
}
