using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public GameObject endScreen;

    [Header("Player")]
    public GameObject player;
    public Player playerScript;
    public Room currentRoomCheckpoint;

    public CanvasGroup fadeCanvas;
    public float fadeDuration = 0.5f;

    private bool transitioning;


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
    public void Teleport(Vector3 spawnPoint)
    {
        player.transform.position = spawnPoint;
    }

    public void CompleteStage()
    {
        currentStage++;
    }

    public void PlayerDied()
    {
        if (!transitionRunning)
            StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        transitionRunning = true;

        deathScreen.SetActive(true);

        yield return new WaitForSeconds(2f);

        //Teleport(currentCheckpoint.position);

        //playerScript.PlayerHealthStats.Heal(playerScript.PlayerHealthStats.MaxHealth);
        //playerScript.isDead = false;
        //playerScript.StateMachine.ChangeState(playerScript.IdleState);

        playerScript.Respawn();

        deathScreen.SetActive(false);

        transitionRunning = false;
    }

    public void LevelCompleted()
    {
        if (!transitionRunning)
            StartCoroutine(LevelCompleteRoutine());
    }

    IEnumerator LevelCompleteRoutine()
    {
        transitionRunning = true;

        if (currentStage < 2)
            successScreen.SetActive(true);
        else
            endScreen.SetActive(true);

            yield return new WaitForSeconds(2f);
        
        successScreen.SetActive(false);
        if (currentStage >= 2)
            SceneManager.LoadScene(1);

        transitionRunning = false;
        playerScript.Heal();
    }

    public void Transition(Room targetRoom)
    {
        if (!transitioning)
            StartCoroutine(TransitionRoutine(targetRoom));
    }

    private IEnumerator TransitionRoutine(Room targetRoom)
    {
        transitioning = true;

        // Fade to black
        yield return Fade(1f);

        // Teleport while screen is black
        Teleport(targetRoom.spawnPoint);
        EnterRoom(targetRoom);

        // Fade back in
        yield return Fade(0f);

        transitioning = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvas.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = targetAlpha;
    }
}