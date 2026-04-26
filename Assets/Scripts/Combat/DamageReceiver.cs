using UnityEngine;

public class DamageReceiver : MonoBehaviour, IDamageable
{
    private Stats stats;

    public void Damage(float amount)
    {
        Debug.Log(gameObject.name + " Damaged!");
        stats?.DecreaseHealth(amount);
    }

    private void Awake()
    {
        stats = GetComponent<Stats>();
    }
}