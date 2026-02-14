using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region TODO
    //TODO
    //  De sters override uri inutile
    //  Animatie dash
    //
    #endregion

    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public PlayerDashState DashState { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody RB { get; private set; }
    public PlayerInputManager PlayerInput { get; private set; }

    #endregion

    #region Other variables
    public Vector3 CurrentVelocity { get; private set; }

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
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
    #endregion

    #region Set functions
    public void SetVelocity(Vector3 velocity)
    {
        CurrentVelocity = velocity;
        RB.linearVelocity = CurrentVelocity;
       
    }

    #endregion

    #region Get Funcions
    public Vector3 GetLookDir()
    {
        Vector2 input = PlayerInput.MoveInput;
        if (input != Vector2.zero)
        {
            return new Vector3(input.x, 0, input.y);
        }
        else
        {
            return Vector3.zero;
        }
    }
    #endregion

    #region Other Functions
    public void AddForceInDirrection(Vector3 force)
    {
        RB.AddForce(force);
    }
    #endregion
}
