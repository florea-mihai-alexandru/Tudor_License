using UnityEngine;

public class Door : MonoBehaviour
{
    public Room targetRoom;

    public Transform targetSpawnPoint;

    public bool unlocked = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!unlocked)
            return;

        RoomManager.Instance.Teleport(targetRoom, targetSpawnPoint);
    }
}