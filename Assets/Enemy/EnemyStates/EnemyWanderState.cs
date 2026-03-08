using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : EnemyState
{
    protected float timePassed;
    protected Vector3 randomDirection;

    public Transform centrePoint;
    public EnemyWanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Transform centerPoint) : base(enemy, stateMachine, enemyData, animBoolName)
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
        Vector3 point;
        Vector3 centre;
        if (centrePoint == null)
        {
            centre = enemy.CentrePos;
        }
        else
        {
            centre = centrePoint.position;
        }

        if (RandomPoint(centre, enemyData.wanderingDistanceRange, out point)) 
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
            enemy.DesiredDestination = point;
        }
        Debug.Log("rand point = " + point);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit;
        Debug.Log(randomPoint + "random poin ");
        //randomPoint.y = 0;
        if (NavMesh.SamplePosition(randomPoint, out hit, enemyData.wanderingDistanceRange, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        Debug.Log("False rand point ");
        return false;
    }
}
