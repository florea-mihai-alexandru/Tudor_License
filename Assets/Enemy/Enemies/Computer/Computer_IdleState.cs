using UnityEngine;

public class Computer_IdleState : EnemyIdleState
{
    Computer_EN Cenemy;
    public Computer_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, float duration, Computer_EN Cenemy) : base(enemy, stateMachine, enemyData, animBoolName, duration)
    {
        this.Cenemy = Cenemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed >= duration)
        {
            stateMachine.ChangeState(Cenemy.WanderState);
        }
    }
}
