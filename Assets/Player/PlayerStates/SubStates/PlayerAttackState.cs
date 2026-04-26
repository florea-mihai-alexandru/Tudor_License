using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttackState : PlayerAilityState
{
    private Weapon weapon;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, Weapon weapon) 
        : base(player, stateMachine, playerData, animBoolName)
    {
        this.weapon = weapon;

        weapon.OnExit += ExitHandler;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

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
