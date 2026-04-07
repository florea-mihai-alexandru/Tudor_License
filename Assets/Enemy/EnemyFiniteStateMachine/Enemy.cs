using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

public class Enemy : MonoBehaviour
{
    #region State Machine variables
    public EnemyStateMachine StateMachine;

    [SerializeField]
    protected EnemyData enemyData;
    #endregion


    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody RB { get; private set; }
    public NavMeshAgent EnemyNavMeshAgent { get; private set; }
    [SerializeField]
    private Transform playerTransform;
    public Transform PlayerTransform { get; private set; }
    public Collider EnemyCollider { get; private set; }
    #endregion


    #region Vectors
    public Vector3 CurrentVelocity { get; private set; }
    public Vector3 FacingDirection { get; private set; }
    public Vector3 DesiredDestination { get; set; }
    public Vector3 CurrentMoveDir { get; private set; }
    public Vector3 DesiredAttackPos { get; private set; }
    #endregion


    #region Other variables
    [SerializeField]
    private LayerMask whatIsOponent;
    public LayerMask WhatIsOponent { get; private set; }

    [SerializeField]
    private LayerMask npcLayerMask;

    [SerializeField]
    private LayerMask whatIsWall;

    [SerializeField]
    public Transform attackPos;
    public Vector3 CentrePos {get; private set; }

    [SerializeField]
    private Transform navMeshTransform;
    public Vector3 NavMeshOffset { get; private set; }
    public ContextSteering AI_ContextSteering { get; private set; }
    public float originalMass;
    public float attackingMass;
    #endregion


    #region Unity callback functions
    public virtual void Start()
    {
        RB = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
        
        EnemyNavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        EnemyNavMeshAgent.updatePosition = false;
        EnemyNavMeshAgent.updateRotation = false;

        if (attackPos ==  null)
        {
            attackPos = FindChildWithTag(this.gameObject, "AttackPos").transform;
        }

        EnemyCollider = GetComponentInChildren<Collider>();
        if (EnemyCollider != null)
        {
            CentrePos = EnemyCollider.bounds.center;
        }
        Debug.Log(CentrePos);
     
        PlayerTransform = playerTransform;
        FacingDirection = new Vector3(1, 0, 0);

        WhatIsOponent = whatIsOponent;

        NavMeshOffset = navMeshTransform.position - transform.position;

        StateMachine = new EnemyStateMachine();
        AI_ContextSteering = new ContextSteering(this, CentrePos, whatIsWall, npcLayerMask, 16);
        
        originalMass = RB.mass;
        attackingMass = originalMass * 100f;
    }

    public virtual void Update()
    {
        AI_ContextSteering.centrePos = CentrePos;
        StateMachine.CurrentState.LogicUpdate();
    }
    public virtual void FixedUpdate()
    {
        if (EnemyCollider != null)
        {
            CentrePos = EnemyCollider.bounds.center;
        }
        EnemyNavMeshAgent.nextPosition = RB.position + NavMeshOffset;
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion


    #region Set functions
    public void SetVelocity(Vector3 velocity)
    {
        CurrentVelocity = velocity;
        RB.linearVelocity = CurrentVelocity;
    }
    #endregion

    #region Get functions
    public float distanceToPlayer()
    {
        //Debug.Log((CenterPos - playerTransform.position).magnitude);
        return (CentrePos - playerTransform.position).magnitude;
    }

    public Vector3 toPlayerVector()
    {
        return CentrePos - playerTransform.position;
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }
    #endregion


    #region Action functions
    public virtual void performAttack()
    {
        Debug.Log("Attacked");
    }

    public virtual void MoveTowardsDest(float speed)
    {
        EnemyNavMeshAgent.SetDestination(DesiredDestination);
        Vector3 desired = EnemyNavMeshAgent.desiredVelocity;
        Vector3 navDir = desired.sqrMagnitude > 0.001f ? desired.normalized : Vector3.zero;
        Vector3 contextDir = AI_ContextSteering.GetDirection(navDir);

        CurrentMoveDir = Vector3.Lerp(
            CurrentMoveDir,
            contextDir,
            Time.deltaTime * 6f);

        SetVelocity(CurrentMoveDir * speed);
    }
    #endregion

    #region Check functions
    public bool CheckIfNavmeshArrived()
    {
        float dist = EnemyNavMeshAgent.remainingDistance;
        if (EnemyNavMeshAgent.remainingDistance <= EnemyNavMeshAgent.stoppingDistance)
            return true;
        return false;
    }
    #endregion

    #region Draw functions
    public virtual void OnDrawGizmosSelected()
    {
        if (attackPos != null && enemyData != null)
        {
            // AOE Gizmos
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, enemyData.AoE_Radius);

            Gizmos.color = Color.aliceBlue;
            Gizmos.DrawWireSphere(CentrePos, enemyData.chaseDisThresh);
        }
    }

    void OnDrawGizmos()
    {
        if (AI_ContextSteering == null) return;

        Vector3 origin = CentrePos;

        var dirs = AI_ContextSteering.Directions;
        var interest = AI_ContextSteering.Interest;
        var danger = AI_ContextSteering.Danger;

        for (int i = 0; i < dirs.Length; i++)
        {
            Vector3 worldDir = transform.TransformDirection(dirs[i]);

            float interestLength = interest[i];
            float dangerLength = danger[i];

            // Interest (green)
            Gizmos.color = Color.green;
            Gizmos.DrawLine(origin, origin + worldDir * interestLength * 2f);

            // Danger (red)
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, origin + worldDir * dangerLength * 2f);
        }

        // Final chosen direction
        Gizmos.color = Color.blue;
        Vector3 finalDir = transform.TransformDirection(AI_ContextSteering.LastChosenDirection);
        Gizmos.DrawLine(origin, origin + finalDir * 3f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, AI_ContextSteering.NpcAvoidRadius);
    }
    #endregion

}
