using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected Vector3 attackPos;
    protected bool attackDone = false;
    bool preWindup;

    protected float preWindupTime = 0f;
    protected float attackTime = 0f;

    protected float attackDuration;
    protected float preWindupDuration;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        attackDuration = enemyData.attackDuration;
        preWindupDuration = enemyData.preWindupDuration;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(Vector3.zero);
        enemy.RB.mass = enemy.attackingMass;

        preWindup = true;
        attackDone = false;

        preWindupTime = 0f;
        attackTime = 0f;

    }

    public override void Exit()
    {
        base.Exit();
        enemy.RB.mass = enemy.originalMass;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (preWindup)
        {
            if (preWindupTime >= preWindupDuration)
            {
                preWindup = false;
            }

            preWindupTime += Time.deltaTime;
        }
        else
        {
            if (attackTime >= attackDuration)
            {
                //Debug.Log("Attack performed on time " + attackTime);
                enemy.performAttack();
                attackDone = true;
            }

            attackTime += Time.deltaTime;
        }

        
    }

   
}
