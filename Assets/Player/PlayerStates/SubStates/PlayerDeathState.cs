using UnityEngine;

public class PlayerDeathState : PlayerState
{
    float duration = 2f;
    float timer = 0f;
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
        timer = duration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //GameObject.Destroy(this.player.gameObject);
            player.levelManager.PlayerDied();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
