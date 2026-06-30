using UnityEngine;

public class CoreBoss : MonoBehaviour
{
    public CoreWeakPoint[] weakPoints;

    private ArenaController arenaController;

    private int remaining;

    private void Start()
    {
        arenaController = GetComponentInParent<ArenaController>();

        remaining = weakPoints.Length;

        foreach (CoreWeakPoint point in weakPoints)
        {
            point.OnDestroyed += WeakPointDestroyed;
        }
    }

    private void WeakPointDestroyed(CoreWeakPoint point)
    {
        remaining--;

        if (remaining <= 0)
        {
            arenaController.BossDefeated();

            Destroy(gameObject);
        }
    }
}