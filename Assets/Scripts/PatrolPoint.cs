using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField]
    public float patrolRadius = 30f;
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.limeGreen;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

    }
}
