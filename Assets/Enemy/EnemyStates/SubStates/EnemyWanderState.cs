using UnityEngine;

public class EnemyWanderState : EnemyNormalState
{
    public EnemyWanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, float duration) : base(enemy, stateMachine, enemyData, animBoolName, duration)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed >= duration)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
