using UnityEngine;
using Unity.Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("References")]
    public CinemachineCamera cineCam;

    public Room CurrentRoom { get; private set; }


    public int currentStage = 0;

    [Header("UI")]
    public GameObject deathScreen;
    public GameObject successScreen;

    [Header("Player")]
    public GameObject player;
    public Transform playerTransform;
    public Transform currentCheckpoint;

    private bool transitionRunning = false;

    private void Awake()
    {
        Instance = this;

        //deathScreen.SetActive(false);
        //successScreen.SetActive(false);
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

    //public void PlayerDied()
    //{
    //    if (!transitionRunning)
    //        StartCoroutine(DeathRoutine());
    //}

    //IEnumerator DeathRoutine()
    //{
    //    transitionRunning = true;

    //    deathScreen.SetActive(true);

    //    yield return new WaitForSeconds(2f);

    //    player.position = currentCheckpoint.position;

    //    deathScreen.SetActive(false);

    //    transitionRunning = false;
    //}

    //public void LevelCompleted(Transform nextCheckpoint)
    //{
    //    if (!transitionRunning)
    //        StartCoroutine(LevelCompleteRoutine(nextCheckpoint));
    //}

    //IEnumerator LevelCompleteRoutine(Transform nextCheckpoint)
    //{
    //    transitionRunning = true;

    //    successScreen.SetActive(true);

    //    yield return new WaitForSeconds(2f);

    //    currentCheckpoint = nextCheckpoint;

    //    successScreen.SetActive(false);

    //    transitionRunning = false;
    //}
}