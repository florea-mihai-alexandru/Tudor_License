using UnityEngine;

public class PlayerDeathState : PlayerState
{
    float duration = 2f;
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, float duration) : base(player, stateMachine, playerData, animBoolName)
    {
        this.duration = duration;
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
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            GameObject.Destroy(this.player.gameObject);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
