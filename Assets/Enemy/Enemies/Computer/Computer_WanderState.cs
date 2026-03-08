using UnityEngine;

public class Computer_WanderState : EnemyWanderState
{
    Computer_EN Cenemy;

    public Computer_WanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Transform centerPoint) : base(enemy, stateMachine, enemyData, animBoolName, centerPoint)
    {
        this.Cenemy = (Computer_EN)enemy;     
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed >= enemyData.wanderDuration || enemy.CheckIfNavmeshArrived())
        {
            stateMachine.ChangeState(Cenemy.IdleState);
        }
        //Debug.Log("Distance to player: " + enemy.distanceToPlayer());
        if (enemy.distanceToPlayer() <= enemyData.attackDisThresh)
        {
            stateMachine.ChangeState(Cenemy.AttackState);
        }
        if (enemy.distanceToPlayer() <= enemyData.chaseDisThresh)
        {
            stateMachine.ChangeState(Cenemy.ChaseState);
        }

    }
}
