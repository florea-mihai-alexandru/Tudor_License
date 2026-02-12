using UnityEngine;

public class PlayerMoveState : PlayerNormalState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        if (moveInput.x == 0  && moveInput.y == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (stateMachine.CurrentState != player.DashState) 
            player.SetVelocity(new Vector3(moveInput.x * playerData.playerSpeed, 0, moveInput.y * playerData.playerSpeed));

        player.Anim.SetFloat("xVelocity", moveInput.x);
        player.Anim.SetFloat("yVelocity", moveInput.y);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
