using System;
using UnityEngine;

[ExecuteAlways]
public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
{
    public event Action<Collider[]> OnDetectedCollider;

    private Vector3 offset;
    private Collider[] detected;

    public Vector3 AttackDirection { get; set; } = Vector3.right;

    private void HandleAttackAction()
    {
        // Calculeaza rotatia bazata pe direcția de atac
        Quaternion attackRotation = AttackDirection != Vector3.zero
            ? Quaternion.FromToRotation(Vector3.right, AttackDirection)
            : Quaternion.identity;

        // Roteste center-ul hitbox-ului dupa directia de atac
        Vector3 rotatedCenter = attackRotation * currentAttackData.HitBoxCenter;
        offset = transform.position + rotatedCenter;

        detected = Physics.OverlapBox(
            offset,
            currentAttackData.HitBoxSize / 2f,
            attackRotation,     
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

        Quaternion attackRotation = AttackDirection != Vector3.zero
            ? Quaternion.FromToRotation(Vector3.right, AttackDirection)
            : Quaternion.identity;

        foreach (var item in gizmoData.AttackData)
        {
            if (!item.Debug) continue;

            Vector3 rotatedCenter = attackRotation * item.HitBoxCenter;

            Gizmos.matrix = Matrix4x4.TRS(
                transform.position + rotatedCenter,
                attackRotation,
                Vector3.one
            );
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, item.HitBoxSize);
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}