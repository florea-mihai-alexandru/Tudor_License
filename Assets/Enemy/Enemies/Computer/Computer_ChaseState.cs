using UnityEngine;

public class Computer_ChaseState : EnemyChaseState
{
    Computer_EN Cenemy;
    public Computer_ChaseState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        Cenemy = (Computer_EN) enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.distanceToPlayer() <= enemyData.attackDisThresh)
        {
            stateMachine.ChangeState(Cenemy.AttackState);
        }

        if (enemy.CheckIfNavmeshArrived())
        {
            stateMachine.ChangeState(Cenemy.IdleState);
        }
    }

}
