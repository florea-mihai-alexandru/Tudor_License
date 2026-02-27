using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyWanderState : EnemyState
{
    protected float duration;
    protected float timePassed;
    public EnemyWanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, float duration) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.duration = duration;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        timePassed = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timePassed += Time.deltaTime;

        //enemy.SetVelocity(enemy.FacingDirection * enemyData.wanderSpeed);
        enemy.DesiredDestination = enemy.PlayerTransform.position;
        enemy.EnemyNavMeshAgent.SetDestination(enemy.DesiredDestination);

        Vector3 desired = enemy.EnemyNavMeshAgent.desiredVelocity;
        Vector3 dir = desired.sqrMagnitude > 0.001f ? desired.normalized : Vector3.zero;

        enemy.SetVelocity(dir * enemyData.wanderSpeed);

        //enemy.NavMeshAgent.des
        Debug.Log(enemy.EnemyNavMeshAgent.desiredVelocity +  " desired vel");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
