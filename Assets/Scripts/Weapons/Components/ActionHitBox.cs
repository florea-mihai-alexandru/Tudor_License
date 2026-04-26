using System;
using UnityEngine;

[ExecuteAlways]
public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
{
    public event Action<Collider[]> OnDetectedCollider;

    private Vector3 offset;
    private Collider[] detected;

    private void HandleAttackAction()
    {
        offset = transform.position + currentAttackData.HitBoxCenter;

        detected = Physics.OverlapBox(
            offset,
            currentAttackData.HitBoxSize / 2f,
            transform.rotation,          
            data.DetectableLayers
        );

        if (detected.Length == 0)
            return;

        OnDetectedCollider?.Invoke(detected);
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
        var gizmoData = data ?? GetComponent<ActionHitBoxData>();

        if (gizmoData == null) return;

        foreach (var item in gizmoData.AttackData)
        {
            if (!item.Debug) continue;

            Gizmos.matrix = Matrix4x4.TRS(
                transform.position + item.HitBoxCenter,
                transform.rotation,
                Vector3.one
            );
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, item.HitBoxSize);
            Gizmos.matrix = Matrix4x4.identity; 
        }
    }
}