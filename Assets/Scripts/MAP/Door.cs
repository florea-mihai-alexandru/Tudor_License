using UnityEngine;

public class Door : MonoBehaviour
{
    public Room targetRoom;
    public bool unlocked = true;
    public SpriteRenderer sr;
    public bool skipTransition = false;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void doorEn()
    {
        unlocked = true;
        sr.enabled = true;
        Debug.Log(sr);
    }

    public void doorDis()
    {
        unlocked = false;
        sr.enabled = false;
        Debug.Log(sr);
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (!other.CompareTag("Player"))
            return;

        if (!unlocked)
            return;

        if (!skipTransition)
            LevelManager.Instance.Transition(targetRoom);
        else
            LevelManager.Instance.Teleport(targetRoom.spawnPoint);
        LevelManager.Instance.EnterRoom(targetRoom);
    }
}