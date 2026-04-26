using UnityEngine;

public class Damage : WeaponComponent<DamageData, AttackDamage>
{
    private ActionHitBox hitBox;

    private void HandleDetectCollider(Collider[] colliders)
    {
        foreach(var item in colliders)
        {
            if(item.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(currentAttackData.Amount);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        hitBox = GetComponent<ActionHitBox>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        hitBox.OnDetectedCollider += HandleDetectCollider;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        hitBox.OnDetectedCollider -= HandleDetectCollider;
    }
}
