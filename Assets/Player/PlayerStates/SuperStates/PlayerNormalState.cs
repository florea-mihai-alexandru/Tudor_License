using UnityEngine;

public class PlayerNormalState : PlayerState
{
    protected Vector3 input;
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
        input = player.PlayerInput.MoveInput;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
