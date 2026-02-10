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
        if (input.x == 0  && input.y == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        player.SetVelocity(new Vector3(input.x * playerData.playerSpeed, 0, input.y * playerData.playerSpeed));

        player.Anim.SetFloat("xVelocity", input.x);
        player.Anim.SetFloat("yVelocity", input.y);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
