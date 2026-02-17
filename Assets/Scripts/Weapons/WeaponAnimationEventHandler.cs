using System;
using UnityEngine;

public class WeaponAnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    private void AnimationFinishedTrigger() => OnFinish?.Invoke();
}
