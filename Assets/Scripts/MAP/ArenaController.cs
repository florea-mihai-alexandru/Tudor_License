using UnityEngine;

public class ArenaController : MonoBehaviour
{
    public Door exitDoor;
    public Door bossDoor;

    //private int currentWave = 0;

    private void Start()
    {
        exitDoor.doorDis();
        bossDoor.doorDis();
    }

    public void EncounterFinished()
    {
        bossDoor.doorEn();
    }

    public void BossDefeated()
    {
        exitDoor.doorEn();
        LevelManager.Instance.CompleteStage();
    }
}