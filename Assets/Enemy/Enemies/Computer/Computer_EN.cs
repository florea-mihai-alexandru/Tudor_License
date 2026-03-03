using UnityEngine;

public class Computer_EN : Enemy
{
    public Computer_IdleState IdleState { get; private set; }
    public Computer_WanderState WanderState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    /// <summary> TODO
    ///  De facut wander state: 
    ///     - de ales punct random in navmesh
    ///     - de luat directia corecta catre punct 
    /// </summary>

    public override void Start()
    {
        base.Start();
        IdleState = new Computer_IdleState(this, StateMachine, enemyData, "idle", 2f, this);
        WanderState = new Computer_WanderState(this, StateMachine, enemyData, "walk", 2f, this);
        AttackState = new EnemyAttackState(this, StateMachine, enemyData, "attack");
        

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
