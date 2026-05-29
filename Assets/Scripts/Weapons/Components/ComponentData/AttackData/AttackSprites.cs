using System;
using UnityEngine;

[Serializable]
public class AttackSprites : AttackData
{
    [field: SerializeField] public Sprite[] SpritesRight { get; private set; }
    [field: SerializeField] public Sprite[] SpritesLeft { get; private set; }
    [field: SerializeField] public Sprite[] SpritesUp { get; private set; }
    [field: SerializeField] public Sprite[] SpritesDown { get; private set; }

    public Sprite[] GetSpritesForDirection(int dirIndex) => dirIndex switch
    {
        0 => SpritesRight,
        1 => SpritesRight,
        2 => SpritesUp,
        3 => SpritesDown,
        _ => SpritesRight
    };
}