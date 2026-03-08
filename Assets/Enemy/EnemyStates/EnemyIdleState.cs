using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected float duration;
    protected float timePassed;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, float duration) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.duration = duration;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        timePassed = 0;
        enemy.SetVelocity(Vector3.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timePassed += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
