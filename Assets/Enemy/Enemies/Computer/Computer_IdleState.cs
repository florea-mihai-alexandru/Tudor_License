using UnityEngine;

public class Computer_IdleState : EnemyIdleState
{
    Computer_EN Cenemy;
    public Computer_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.Cenemy = (Computer_EN)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed >= enemyData.idleDuration)
        {
            stateMachine.ChangeState((Cenemy.WanderState));
        }
        if (enemy.distanceToPlayer() <= enemyData.chaseDisThresh)
        {
            stateMachine.ChangeState(Cenemy.ChaseState);
        }
    }
}
