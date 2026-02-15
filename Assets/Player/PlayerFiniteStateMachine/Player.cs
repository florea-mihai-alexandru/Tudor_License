using Mono.Cecil.Cil;
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
    public TrailRenderer DashTrail { get; private set; }
    public SpriteRenderer PlayerSprite { get; private set; }
    public HealthStats PlayerHealthStats { get; private set; }

    #endregion

    #region Other variables
    public Vector3 CurrentVelocity { get; private set; }

    [SerializeField]
    private Transform playerSpriteTransform;

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

        DashTrail = GetComponentInChildren<TrailRenderer>();
        if (DashTrail != null) 
            DashTrail.enabled = false;

        PlayerSprite = playerSpriteTransform.GetComponent<SpriteRenderer>();

        PlayerHealthStats = GetComponent<HealthStats>();
        Debug.Log("Player health stats: " + PlayerHealthStats.Health + "health " + PlayerHealthStats.MaxHealth);

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

    #region Check Functions
    public bool CheckIfShouldFlip()
    {
        Vector3 LookDir = GetLookDir();
        //Debug.Log(LookDir + "LOOKDIR");
        if (LookDir.x < 0)
            return true;
        return false;
    }
    #endregion

    #region Other Functions
    public void AddForceInDirrection(Vector3 force)
    {
        RB.AddForce(force);
    }
    #endregion
}
