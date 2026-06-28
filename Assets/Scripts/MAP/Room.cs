using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Camera")]
    public float roomFOV = 60f;
    public float camRot = 45f;
    public Color backColor = new Color32(72, 64, 56, 0);

    [Header("Encounter")]
    public GameObject encounterObject;

    [Header("Settings")]
    public bool activateEncounterOnEnter = true;
    public bool onlyActivateOnce = true;

    private bool activated = false;

    [Header("Spawn Points")]
    public Transform spawnPoint;

    public virtual void EnterRoom()
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