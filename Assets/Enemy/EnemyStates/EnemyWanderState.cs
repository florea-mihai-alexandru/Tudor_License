using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyWanderState : EnemyState
{
    protected float duration;
    protected float timePassed;
    protected Vector3 randomDirection;
    public EnemyWanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, float duration) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.duration = duration;
    }

    public override void Enter()
    {
        base.Enter();
        timePassed = 0;

        float randomX = Random.Range(-1, 1);
        float randomY = Random.Range(-1, 1);

        randomDirection = new Vector3(randomX, 0, randomY);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timePassed += Time.deltaTime;

        enemy.SetVelocity(randomDirection * enemyData.wanderSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
