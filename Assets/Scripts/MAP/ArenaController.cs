using UnityEngine;

public class ArenaController : MonoBehaviour
{
    public Door exitDoor;
    public Door bossDoor;

    //private int currentWave = 0;

    private void Start()
    {
        exitDoor.unlocked = false;
        bossDoor.unlocked = false;
    }

    public void EncounterFinished()
    {
        bossDoor.unlocked = true;   
    }

    public void BossDefeated()
    {
        exitDoor.unlocked = true;
        LevelManager.Instance.CompleteStage();
    }
}