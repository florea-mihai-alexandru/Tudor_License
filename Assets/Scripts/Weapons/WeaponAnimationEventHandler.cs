using System;
using UnityEngine;

public class WeaponAnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnAttackAction;
    private void AnimationFinishedTrigger() => OnFinish?.Invoke();
    private void AttackActionTrigger() => OnAttackAction?.Invoke();
}
