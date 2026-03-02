using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine StateMachine;

    [SerializeField]
    protected EnemyData enemyData;

    public Animator Anim { get; private set; }
    public Rigidbody RB { get; private set; }
    public NavMeshAgent EnemyNavMeshAgent { get; private set; }

    public Vector3 CurrentVelocity { get; private set; }
    public Vector3 FacingDirection { get; private set; }
    public Vector3 DesiredDestination { get; set; }

    [SerializeField]
    private Transform playerTransform;
    public Transform PlayerTransform { get; private set; }

    [SerializeField]
    private Transform navMeshTransform;
    public Vector3 NavMeshOffset { get; private set; }

    public virtual void Start()
    {
        RB = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
        
        EnemyNavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        EnemyNavMeshAgent.updatePosition = false;
        EnemyNavMeshAgent.updateRotation = false;

        PlayerTransform = playerTransform;
        FacingDirection = new Vector3(1, 0, 0);

        NavMeshOffset = navMeshTransform.position - transform.position;
        Debug.Log(NavMeshOffset + "OFFSET MESH");

        StateMachine = new EnemyStateMachine();
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    public virtual void FixedUpdate()
    {
        EnemyNavMeshAgent.nextPosition = RB.position + NavMeshOffset;
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocity(Vector3 velocity)
    {
        CurrentVelocity = velocity;
        RB.linearVelocity = CurrentVelocity;
    }

}
