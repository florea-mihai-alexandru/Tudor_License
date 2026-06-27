using UnityEngine;

public class ArenaEncounter : EncounterController
{
    public HealthStats crystalHealth;

    protected override void WaveCompleted()
    {
        crystalHealth.TakeDamage(1);

        base.WaveCompleted();
    }

    protected override void EncounterCompleted()
    {
        Debug.Log("Arena Cleared");

        arenaController.EncounterFinished();
        // unlock boss teleporter
    }
}
