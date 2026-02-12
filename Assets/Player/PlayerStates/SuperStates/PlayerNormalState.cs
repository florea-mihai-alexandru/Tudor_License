using UnityEngine;

public class PlayerNormalState : PlayerState
{
    protected Vector3 moveInput;
    public PlayerNormalState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        moveInput = player.PlayerInput.MoveInput;
        
        if (player.PlayerInput.DashInput)
        {
            stateMachine.ChangeState(player.DashState);
            player.PlayerInput.DashUsed();
        }

        if (player.PlayerInput.AttackInput)
        {
            stateMachine.ChangeState(player.AttackState);
            player.PlayerInput.AttackUsed();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
