using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks(); 
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.distanceToPlayer() <= enemyData.chaseDisThresh)
        {
            if (enemy.PlayerTransform != null)
            {
                 enemy.DesiredDestination = enemy.PlayerTransform.position;
            }
        }
        enemy.MoveTowardsDest(enemyData.chaseSpeed);
        
        //enemy.EnemyNavMeshAgent.SetDestination(enemy.DesiredDestination);

        //Vector3 desired = enemy.EnemyNavMeshAgent.desiredVelocity;
        //Vector3 dir = desired.sqrMagnitude > 0.001f ? desired.normalized : Vector3.zero;

        //enemy.SetVelocity(dir * enemyData.chaseSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
