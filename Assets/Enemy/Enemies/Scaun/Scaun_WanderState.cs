using UnityEngine;

public class Scaun_WanderState : EnemyWanderState
{
    Scaun_EN Senemy;

    public Scaun_WanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, PatrolPoint centerPoint) : base(enemy, stateMachine, enemyData, animBoolName, centerPoint)
    {
        this.Senemy = (Scaun_EN)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed >= enemyData.wanderDuration || enemy.CheckIfNavmeshArrived())
        {
            stateMachine.ChangeState(Senemy.IdleState);
        }
        //Debug.Log("Distance to player: " + enemy.distanceToPlayer());
        if (enemy.distanceToPlayer() <= enemyData.attackDisThresh)
        {
            stateMachine.ChangeState(Senemy.IdleState);
        }
        if (enemy.distanceToPlayer() <= enemyData.chaseDisThresh)
        {
            stateMachine.ChangeState(Senemy.ChaseState);
        }

    }
}

