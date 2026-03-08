using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Distance thresholds")]
    public float chaseDisThresh = 35f;
    public float attackDisThresh = 15f;

    [Header("Wander State")]
    public float wanderSpeed = 50f;

    [Header("Chase State")]
    public float chaseSpeed = 75f;

    [Header("Attack State")]
    public float attackDuration = 0.5f;
    public float preWindupDuration = 1f;
    public float AoE_Radius = 15f;
    public float Damage = 1;
}
