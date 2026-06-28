using UnityEngine;

public class PlayerIdleState : PlayerNormalState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        //player.SetVelocity(Vector3.zero);
        player.PlayerHealthStats.OnDamageTaken += player.HandleDamageTaken;
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayerHealthStats.OnDamageTaken -= player.HandleDamageTaken;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
