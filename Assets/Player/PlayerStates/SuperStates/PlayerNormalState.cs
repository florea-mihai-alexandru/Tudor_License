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
        player.PlayerSprite.flipX = false;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
