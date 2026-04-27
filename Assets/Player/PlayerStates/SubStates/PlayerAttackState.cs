using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttackState : PlayerAilityState
{
    private Weapon weapon;
    private ActionHitBox actionHitBox;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, Weapon weapon, ActionHitBox actionHitBox) 
        : base(player, stateMachine, playerData, animBoolName)
    {
        this.weapon = weapon;
        this.actionHitBox = actionHitBox;

        weapon.OnExit += ExitHandler;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if (actionHitBox != null)
        {
            Vector3 moveDir = player.LastMoveDirection;
            actionHitBox.AttackDirection = moveDir != Vector3.zero ? moveDir : player.transform.right; 
        }

        weapon.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        abilityDone = true;
    }
}
