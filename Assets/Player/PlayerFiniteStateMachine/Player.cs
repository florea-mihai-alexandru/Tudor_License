using Mono.Cecil.Cil;
using System.Linq;
using UnityEngine;
using Unity.Cinemachine;

using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region TODO
    //TODO
    //  De sters override uri inutile
    #endregion

    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public PlayerDashState DashState { get; private set; }

    public PlayerAttackState AttackState {  get; private set; }
    public PlayerDeathState DeathState { get; private set; }

    public PlayerDamaged DamagedState {  get; private set; }

    private DamageFlash damageFlash;
    public DamageFlash DamageFlash => damageFlash;

    [SerializeField]
    private PlayerData playerData;

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody RB { get; private set; }
    public PlayerInputManager PlayerInput { get; private set; }

    public WeaponParent WeaponParent { get; private set; }

    public TrailRenderer DashTrail { get; private set; }
    public SpriteRenderer PlayerSprite { get; private set; }
    public HealthStats PlayerHealthStats { get; private set; }

    #endregion

    #region Other variables
    public Vector3 CurrentVelocity { get; private set; }
    //[SerializeField] public GameHandler gameHandler;

    private Weapon weapon;

    [SerializeField]
    private Transform playerSpriteTransform;

    public Vector3 LastMoveDirection { get; private set; } = Vector3.right;
    private ActionHitBox actionHitBox;
    public bool isDead = false;

    public bool inBoss = false;
    [SerializeField] 
    private CinemachineCamera cineCam;

    public LevelManager levelManager;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        weapon = transform.Find("Weapon").GetComponent<Weapon>();

        actionHitBox = GetComponentInChildren<ActionHitBox>();
        damageFlash = GetComponentInChildren<DamageFlash>(); //

        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "empty", weapon, actionHitBox);
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death", 2f);
        DamagedState = new PlayerDamaged(this, StateMachine, playerData, "idle", 0.5f);
    }

    private void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        PlayerInput = GetComponent<PlayerInputManager>();
        RB = GetComponent<Rigidbody>();
        WeaponParent = GetComponentInChildren<WeaponParent>();

        DashTrail = GetComponentInChildren<TrailRenderer>();
        if (DashTrail != null) 
            DashTrail.enabled = false;

        PlayerSprite = playerSpriteTransform.GetComponent<SpriteRenderer>();

        PlayerHealthStats = GetComponent<HealthStats>();

        //Debug.Log("Player health stats: " + PlayerHealthStats.Health + "health " + PlayerHealthStats.MaxHealth);

        StateMachine.Initialize(IdleState);

        //PlayerHealthStats.canTakeDamage = false; //TODO REMOVE
    }

    private void Update()
    {
        CurrentVelocity = RB.linearVelocity;

        if (DialogueManager.Instance != null && DialogueManager.Instance.IsActive)
        {
            SetVelocity(Vector3.zero);
            return;
        }

        if (PlayerInput.InteractInput)
        {
            PlayerInput.InteractUsed();

            bool canOpenDialogue = DialogueManager.Instance == null
                || (!DialogueManager.Instance.IsActive && DialogueManager.Instance.CanInteract);

            if (canOpenDialogue)
            {
                DialogueTrigger trigger = FindObjectsByType<DialogueTrigger>(FindObjectsSortMode.None)
                    .FirstOrDefault(t => t.IsPlayerInRange());
                if (trigger != null)
                    DialogueManager.Instance.StartDialogue(trigger.dialogueData, trigger.BubbleAnchor);
            }
        }

        Vector3 moveDir = new Vector3(PlayerInput.MoveInput.x, 0f, PlayerInput.MoveInput.y).normalized;
        if (moveDir != Vector3.zero)
            LastMoveDirection = moveDir;

        if (PlayerHealthStats.health <= 0)
            isDead = true;

        if (cineCam != null)
        {
            if (inBoss)
            {
                // FOV
                cineCam.Lens.FieldOfView = 90f;

                // Rotation
                cineCam.transform.rotation = Quaternion.Euler(30f, 0f, 0f);
            }
            else
            {
                cineCam.Lens.FieldOfView = 60f;
                cineCam.transform.rotation = Quaternion.Euler(45f, 0f, 0f);
            }
        }

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

    public int GetAttackDirectionIndex()
    {
        Vector3 dir = LastMoveDirection;
        if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.z))
            return dir.x >= 0 ? 0 : 1;
        else
            return dir.z >= 0 ? 2 : 3;
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

    public void HandleDamageTaken(float damageAmount)
    {
        //Debug.Log($"Damage luat in Idle: {damageAmount}");
        StateMachine.ChangeState(DamagedState);
    }

    public void Respawn()
    {
        levelManager.Teleport(levelManager.currentRoomCheckpoint.spawnPoint.position);
        levelManager.EnterRoom(levelManager.currentRoomCheckpoint);

        PlayerHealthStats.Heal(PlayerHealthStats.MaxHealth);
        isDead = false;
        StateMachine.ChangeState(IdleState);
    }

    public void Heal()
    {
        PlayerHealthStats.Heal(PlayerHealthStats.MaxHealth);
    }

    #endregion
}
