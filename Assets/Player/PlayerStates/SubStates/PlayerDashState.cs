using UnityEngine;

public class PlayerDashState : PlayerAilityState
{
    public float runTime;
    public float dashTime;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        dashTime = playerData.dashTime;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if (player.DashTrail != null) 
            player.DashTrail.enabled = true;

        if (player.CheckIfShouldFlip()) 
            player.PlayerSprite.flipX = true;

        player.SetVelocity(player.GetLookDir() * playerData.playerSpeed * 3);
    }

    public override void Exit()
    {
        base.Exit();

        if (player.DashTrail != null)
            player.DashTrail.enabled = false;
        //player.SetVelocity(Vector3.zero);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        runTime = Time.time - startTime;

        if (runTime >= dashTime) 
            abilityDone = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
