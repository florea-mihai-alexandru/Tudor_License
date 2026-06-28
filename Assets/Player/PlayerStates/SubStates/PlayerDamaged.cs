using UnityEngine;

public class PlayerDamaged : PlayerNormalState
{
    private float stunDuration;
    private float stunTime = 0f;
    public PlayerDamaged(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, float stunDuration) : base(player, stateMachine, playerData, animBoolName)
    {
        this.stunDuration = stunDuration;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        stunTime = stunDuration;
        player.DamageFlash?.Flash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        stunTime -= Time.deltaTime;
        if (stunTime <= 0f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
