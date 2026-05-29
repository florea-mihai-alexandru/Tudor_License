using UnityEngine;

public class Door : MonoBehaviour
{
    public Room targetRoom;

    public Transform targetSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RoomManager.Instance.Teleport(targetRoom, targetSpawnPoint);
    }
}