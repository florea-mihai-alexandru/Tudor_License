using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
[Serializable]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeReference] public List<ComponentData> ComponentData {  get; private set; }

    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();  
    }

    public void AddData(ComponentData data)
    {
        if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
            return;

        ComponentData.Add(data);
    }
}
