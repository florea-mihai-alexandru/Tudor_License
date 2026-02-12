using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float playerSpeed=100;
    [Header("Dash State")]
    public float dashPower = 300;
    public float dashTime = 0.15f;
}
