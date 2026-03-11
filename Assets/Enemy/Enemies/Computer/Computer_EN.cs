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
}
