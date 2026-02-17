/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class HealthBarHUDTester : MonoBehaviour
{
    public void AddHealth()
    {
        HealthStats.Instance.AddHealth();
    }

    public void Heal(float health)
    {
        HealthStats.Instance.Heal(health);
    }

    public void Hurt(float dmg)
    {
        HealthStats.Instance.TakeDamage(dmg);
    }
}
