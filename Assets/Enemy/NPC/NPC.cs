using UnityEngine;

public class NPC : Enemy
{
    public NPC_IdleState IdleState { get; private set; }
    public NPC_WanderState WanderState { get; private set; }

    [SerializeField]
    public PatrolPoint centerPatrolPoint;

    public override void Start()
    {
        base.Start();
        IdleState = new NPC_IdleState(this, StateMachine, enemyData, "idle");
        WanderState = new NPC_WanderState(this, StateMachine, enemyData, "move", centerPatrolPoint);

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
