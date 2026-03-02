using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected Vector3 attackPos;
    protected bool attackDone = false;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        attackPos = enemy.PlayerTransform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

   
}
