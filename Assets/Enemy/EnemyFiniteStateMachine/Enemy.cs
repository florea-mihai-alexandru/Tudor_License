using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine StateMachine;

    [SerializeField]
    protected EnemyData enemyData;

    public Animator Anim { get; private set; }
    public Rigidbody RB { get; private set; }

    public virtual void Start()
    {
        RB = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();

        StateMachine = new EnemyStateMachine();
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
}
