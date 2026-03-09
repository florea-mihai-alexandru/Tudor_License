using System;
using UnityEngine;

public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
{
    private event Action<Collider[]> OnDetectedCollider;

    private Vector3 offset;

    private Collider[] detected;
    private void HandleAttackAction()
    {
        offset.Set(
            transform.position.x + (currentAttackData.HitBox.center.x),
            transform.position.y + currentAttackData.HitBox.center.y,
            transform.position.z
            );

        detected = Physics.OverlapBox(offset, currentAttackData.HitBox.size / 2f, Quaternion.identity, data.DetectableLayers);

        if (detected.Length == 0)
            return;

        OnDetectedCollider?.Invoke(detected);

        foreach (var item in detected)
        {
            Debug.Log(item.name);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        eventHandler.OnAttackAction += HandleAttackAction;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        eventHandler.OnAttackAction -= HandleAttackAction;
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null)
            return;

        foreach (var item in data.AttackData)
        {
            if (!item.Debug)
                continue;

            Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
        }
    }
}
