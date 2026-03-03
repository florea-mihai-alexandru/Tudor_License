using UnityEngine;

public class Computer_AttackState : EnemyAttackState
{
    Computer_EN Cenemy;
    public Computer_AttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        Cenemy = (Computer_EN)enemy;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (attackDone)
        {
            stateMachine.ChangeState(Cenemy.IdleState);
        }
    }
}
