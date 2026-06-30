using UnityEngine;

public class Arena3 : MonoBehaviour
{
    public ArenaController arenaController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arenaController.EncounterFinished();
    }

    
}
