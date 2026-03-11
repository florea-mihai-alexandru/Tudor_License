using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : EnemyState
{
    protected float timePassed;
    protected Vector3 randomDirection;

    public PatrolPoint centrePoint;
    public EnemyWanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, PatrolPoint centerPoint) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.centrePoint = centerPoint;
    }

    public override void Enter()
    {
        base.Enter();
        timePassed = 0;

        NewDestination();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timePassed += Time.deltaTime;

        enemy.MoveTowardsDest(enemyData.wanderSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void NewDestination()
    {
        Vector3 centre;
        float range;
        if (centrePoint == null)
        {
            centre = enemy.CentrePos;
            range = enemyData.wanderingDistanceRange;
        }
        else
        {
            centre = centrePoint.transform.position;
            range = centrePoint.patrolRadius;
        }

        if (RandomPoint(centre, range, out Vector3 point)) 
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
            enemy.DesiredDestination = point;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
