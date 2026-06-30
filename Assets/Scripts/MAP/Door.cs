using UnityEngine;

public class Door : MonoBehaviour
{
    public Room targetRoom;
    public bool unlocked = true;

    private void OnTriggerEnter(Collider other)
    {   
        if (!other.CompareTag("Player"))
            return;

        if (!unlocked)
            return;

        LevelManager.Instance.Teleport(targetRoom.spawnPoint);
        LevelManager.Instance.EnterRoom(targetRoom);
    }
}