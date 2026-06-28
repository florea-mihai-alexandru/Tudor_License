using UnityEngine;

public class Scaun_Chase_State : EnemyChaseState
{
    Scaun_EN Senemy;
    public Scaun_Chase_State(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        Senemy = (Scaun_EN)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.distanceToPlayer() <= enemyData.attackDisThresh)
        {
            stateMachine.ChangeState(Senemy.IdleState);
        }

        if (enemy.CheckIfNavmeshArrived())
        {
            stateMachine.ChangeState(Senemy.IdleState);
        }
    }
}
