using UnityEngine;

public class WeakPointBossEncounter : AbstractBossEncounter
{
    public BossWeakPoint[] weakPoints;

    private int remainingWeakPoints;

    protected override void Start()
    {
        remainingWeakPoints = weakPoints.Length;

        foreach (BossWeakPoint point in weakPoints)
            point.OnDestroyed += WeakPointDestroyed;

        base.Start();
    }

    private void WeakPointDestroyed(BossWeakPoint point)
    {
        remainingWeakPoints--;
    }

    protected override void SetObjectiveDamageable(bool value)
    {
        foreach (BossWeakPoint point in weakPoints)
        {
            if (point != null)
                point.SetDamageable(value);
        }
    }

    protected override bool ObjectiveCompleted()
    {
        return remainingWeakPoints <= 0;
    }
}