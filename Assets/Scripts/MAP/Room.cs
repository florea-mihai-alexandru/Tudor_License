using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Camera")]
    public float roomFOV = 60f;
    public float camRot = 45f;

    [Header("Encounter")]
    public GameObject encounterObject;

    [Header("Settings")]
    public bool activateEncounterOnEnter = true;
    public bool onlyActivateOnce = true;

    private bool activated = false;

    public Transform spawnPoint;

    public void EnterRoom()
    {
        if (activateEncounterOnEnter &&
            encounterObject != null &&
            (!activated || !onlyActivateOnce))
        {
            encounterObject.SetActive(true);
            activated = true;
        }
    }
}