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

        Physics.IgnoreLayerCollision(
            player.gameObject.layer,
            //LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("NPC"),
            true
            );

        player.SetVelocity(player.GetLookDir() * playerData.dashPower);
    }

    public override void Exit()
    {
        base.Exit();

        Physics.IgnoreLayerCollision(
            player.gameObject.layer,
            LayerMask.NameToLayer("NPC"),
            false
            );

        if (player.DashTrail != null)
            player.DashTrail.enabled = false;
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
