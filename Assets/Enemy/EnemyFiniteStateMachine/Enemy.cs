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
    public Vector3 DesiredAttackPos { get; private set; }
    #endregion


    #region Other variables
    [SerializeField]
    private LayerMask whatIsOponent;
    public LayerMask WhatIsOponent { get; private set; }

    [SerializeField]
    public Transform attackPos;
    public Vector3 CentrePos {get; private set; }

    [SerializeField]
    private Transform navMeshTransform;
    public Vector3 NavMeshOffset { get; private set; }
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
    }

    public virtual void Update()
    {
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
    {                                                           /// radius of sphere
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

    public virtual void MoveTowardsDest(float speed)
    {
        EnemyNavMeshAgent.SetDestination(DesiredDestination);
        Vector3 desired = EnemyNavMeshAgent.desiredVelocity;
        Vector3 dir = desired.sqrMagnitude > 0.001f ? desired.normalized : Vector3.zero;

        SetVelocity(dir * speed);
    }
    #endregion

    public bool CheckIfNavmeshArrived()
    {
        float dist = EnemyNavMeshAgent.remainingDistance;
        if (EnemyNavMeshAgent.remainingDistance <= EnemyNavMeshAgent.stoppingDistance)
            return true;
        //Debug.Log("remining " + EnemyNavMeshAgent.remainingDistance + " stopping " + EnemyNavMeshAgent.stoppingDistance);
        return false;
    }

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
    #endregion

}
