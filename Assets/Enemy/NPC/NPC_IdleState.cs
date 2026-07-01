using UnityEngine;

public class NPC_IdleState : EnemyIdleState
{
    NPC Cenemy;
    public NPC_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.Cenemy = (NPC)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed >= enemyData.idleDuration)
        {
            stateMachine.ChangeState((Cenemy.WanderState));
        }
    }
}
