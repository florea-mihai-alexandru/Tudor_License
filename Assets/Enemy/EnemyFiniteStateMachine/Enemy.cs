using System.Collections;
using UnityEditor.UIElements;
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
    public Vector3 DesiredAttackPos { get; private set; }

    [SerializeField]
    private Transform playerTransform;
    public Transform PlayerTransform { get; private set; }

    [SerializeField]
    private LayerMask whatIsOponent;
    public LayerMask WhatIsOponent { get; private set; }

    public Transform AttackPos { get; private set; } // TODO GET ATTACK POS

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

        WhatIsOponent = whatIsOponent;

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

    public float distanceToPlayer()
    {
        return (this.transform.position - playerTransform.position).magnitude;
    }

    public virtual void performAttack(Vector3 attackPos)
    {                                                           /// radius of sphere
        Collider[] oponentsToDamage = Physics.OverlapSphere(attackPos, 1, WhatIsOponent);
        Debug.Log("QAELGJKHNJKLGASHEKG");
        if (oponentsToDamage.Length > 0)
            Debug.Log("found op, player pos = " + playerTransform.position + " at pos: " + attackPos);
        else
            Debug.Log("No player found ");
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

    private void OnDrawGizmosSelected()
    {
        //if (attackPos != null)
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(attackPos.position, currentWeapon.attackRange);
        //}
    }


}
