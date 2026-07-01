using UnityEngine;

public class NPC_WanderState : EnemyWanderState
{
    NPC Senemy;

    public NPC_WanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, PatrolPoint centerPoint) : base(enemy, stateMachine, enemyData, animBoolName, centerPoint)
    {
        this.Senemy = (NPC)enemy;
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

        Vector3 curVel = Senemy.RB.linearVelocity;

        
        Senemy.Anim.SetFloat("xVelocity", curVel.x);
        Senemy.Anim.SetFloat("yVelocity", curVel.y);

    }
}

