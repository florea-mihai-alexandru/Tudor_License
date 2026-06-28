using UnityEngine;

public class Scaun_EN : Enemy
{
    public Scaun_IdleState IdleState { get; private set; }
    public Scaun_WanderState WanderState { get; private set; }
    public Scaun_Chase_State ChaseState { get; private set; }

    [SerializeField]
    public PatrolPoint centerPatrolPoint;

    public override void Start()
    {
        base.Start();
        IdleState = new Scaun_IdleState(this, StateMachine, enemyData, "idle");
        WanderState = new Scaun_WanderState(this, StateMachine, enemyData, "walk", centerPatrolPoint);
        ChaseState = new Scaun_Chase_State(this, StateMachine, enemyData, "walk");

        StateMachine.Initialize(IdleState);
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
