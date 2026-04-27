using UnityEngine;

public class Computer_EN : Enemy
{
    public Computer_IdleState IdleState { get; private set; }
    public Computer_WanderState WanderState { get; private set; }
    public Computer_AttackState AttackState { get; private set; }
    public Computer_ChaseState ChaseState { get; private set; }

    [SerializeField]
    public PatrolPoint centerPatrolPoint;

    public override void Start()
    {
        base.Start();
        IdleState = new Computer_IdleState(this, StateMachine, enemyData, "idle");
        WanderState = new Computer_WanderState(this, StateMachine, enemyData, "walk", centerPatrolPoint);
        AttackState = new Computer_AttackState(this, StateMachine, enemyData, "attack");
        ChaseState = new Computer_ChaseState(this, StateMachine, enemyData, "walk");

        StateMachine.Initialize(WanderState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void Update()
    {
        base.Update();
    }

    public override void performAttack()
    {
        base.performAttack();
        /// radius of sphere
        Collider[] oponentsToDamage = Physics.OverlapSphere(attackPos.position, enemyData.AoE_Radius, WhatIsOponent);
        foreach (Collider enemy in oponentsToDamage)
        {
            Vector3 dirToEnemy = (enemy.transform.position - transform.position).normalized;

            HealthStats oponentScript = enemy.GetComponentInChildren<HealthStats>();
            if (oponentScript != null)
            {
                Debug.Log("TOOK DAMAGE");
                oponentScript.TakeDamage(1);
            }
        }
    }
}
