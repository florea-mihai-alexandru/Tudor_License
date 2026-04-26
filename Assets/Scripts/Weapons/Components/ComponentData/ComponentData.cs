using System;
using UnityEngine;

[Serializable]
public class ComponentData
{
    [SerializeField, HideInInspector] private string name;

    public ComponentData()
    {
        SetComponentName();
    }

    public void SetComponentName() => name = GetType().Name;

    public virtual void SetAttackDataNames() {}

    public virtual void InitializeAttackData(int numberOfAttacks) {}
}

[Serializable]
public class ComponentData<T> : ComponentData where T : AttackData
{
    [SerializeField] private T[] attackData;
    public T[] AttackData {  get => attackData; private set =>attackData = value; }

    public override void SetAttackDataNames()
    {
        base.SetAttackDataNames();

        for (var i = 0; i < AttackData.Length; i++)
        {
            AttackData[i].SetAttackName(i + 1);
        }
    }
}
