using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public Animator Anim { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    public Rigidbody RB { get; private set; }

    public PlayerInputManager PlayerInput { get; private set; }

    public Vector3 CurrentVelocity { get; private set; }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }

    private void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        PlayerInput = GetComponent<PlayerInputManager>();
        RB = GetComponent<Rigidbody>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.linearVelocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocity(Vector3 velocity)
    {
        RB.linearVelocity = velocity;
    }
}
