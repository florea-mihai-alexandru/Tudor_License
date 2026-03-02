using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Wander State")]
    public float wanderSpeed = 50f;

    [Header("Attack State")]
    public float attackDuration = 1.5f;
}
