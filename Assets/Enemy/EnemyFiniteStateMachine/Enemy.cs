using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine StateMachine;

    [SerializeField]
    private EnemyData enemyData;
    public EnemyIdleState IdleState { get; private set; }
    public EnemyWanderState WanderState { get; private set; }

    public Animator Anim { get; private set; }
    public Rigidbody RB { get; private set; }

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();

        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle", 2f);
        WanderState = new EnemyWanderState(this, StateMachine, enemyData, "walk", 3f);
        StateMachine.Initialize(IdleState);
    }
    void Start()
    {
        
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
}
