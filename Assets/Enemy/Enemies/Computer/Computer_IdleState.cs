using UnityEngine;

public class Computer_IdleState : EnemyIdleState
{
    Computer_EN Cenemy;
    public Computer_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, float duration) : base(enemy, stateMachine, enemyData, animBoolName, duration)
    {
        this.Cenemy = (Computer_EN)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed >= duration)
        {
            stateMachine.ChangeState((Cenemy.WanderState));
        }
        if (enemy.distanceToPlayer() <= enemyData.chaseDisThresh)
        {
            stateMachine.ChangeState(Cenemy.ChaseState);
        }
    }
}
