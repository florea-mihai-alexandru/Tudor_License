using UnityEngine;
using Unity.Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("References")]
    public CinemachineCamera cineCam;

    public Room CurrentRoom { get; private set; }

    public GameObject player;

    public int currentStage = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void EnterRoom(Room room)
    {
        if (room == null)
            return;

        CurrentRoom = room;

        //cameraController.SetFOV(room.roomFOV);

        //Set Camera specs
        cineCam.Lens.FieldOfView = room.roomFOV;
        cineCam.transform.rotation = Quaternion.Euler(room.camRot, 0f, 0f);

        Camera.main.backgroundColor = room.backColor;

        room.EnterRoom();
    }

    public void Teleport(Transform spawnPoint)
    {
        player.transform.position = spawnPoint.position;
    }

    public void CompleteStage()
    {
        currentStage++;
    }
}