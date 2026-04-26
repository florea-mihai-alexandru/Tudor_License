using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    private Animator anim;
    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(amount + " Damage taken. HP ramas: " + currentHealth);

        if (anim != null)
            anim.SetTrigger("damage");

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}