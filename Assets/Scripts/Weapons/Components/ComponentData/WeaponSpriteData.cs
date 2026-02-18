using UnityEngine;

public class WeaponSpriteData : ComponentData
{
    [field: SerializeField] public AttackSprites[] AttackData {  get; private set; }
}
