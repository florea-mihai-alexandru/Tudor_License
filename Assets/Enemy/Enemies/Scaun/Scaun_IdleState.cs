using UnityEngine;

public class Scaun_IdleState : EnemyIdleState
{
    Scaun_EN Cenemy;
    public Scaun_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.Cenemy = (Scaun_EN)enemy;
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
