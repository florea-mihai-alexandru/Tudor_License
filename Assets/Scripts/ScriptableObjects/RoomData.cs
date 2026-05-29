using UnityEngine;

[CreateAssetMenu(menuName = "Game/Room")]
public class RoomData : ScriptableObject
{
    public string roomName;

    public Transform spawnPoint;

    public DoorLink[] doors;
}

[System.Serializable]
public class DoorLink
{
    public string doorName;

    public RoomData targetRoom;

    public Transform targetSpawnPoint;
}
