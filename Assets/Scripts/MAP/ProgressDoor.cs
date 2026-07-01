using UnityEngine;

public class ProgressDoor : MonoBehaviour
{
    [System.Serializable]
    public class Destination
    {
        public Room targetRoom;
    }

    public Destination[] destinations;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        int stage = LevelManager.Instance.currentStage;

        if (stage >= destinations.Length)
            stage = destinations.Length - 1;

        LevelManager.Instance.Transition(destinations[stage].targetRoom);
        //LevelManager.Instance.Teleport(
        //    destinations[stage].targetRoom.spawnPoint
        //);
        LevelManager.Instance.EnterRoom(destinations[stage].targetRoom);
    }
}