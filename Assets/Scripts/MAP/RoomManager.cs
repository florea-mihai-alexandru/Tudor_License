using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public GameObject player;

    public Room currentRoom;
    
    public int currentYear = 0;

    public Transform doorHandlerTransform;
    public DoorHandler doorHandlerScript;

    private Room[] allRooms;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //allRooms = FindObjectsOfType<Room>(true);

        //ActivateRoom(currentRoom);
        if (currentRoom != null) 
        { 
            if (currentRoom.defaultSpawnPoint != null)
            {
                player.transform.position =
                    currentRoom.defaultSpawnPoint.position;
            }
        }
        

        if (doorHandlerTransform != null)
        {
            doorHandlerScript = doorHandlerTransform.GetComponent<DoorHandler>();
            doorHandlerScript.switchDoor(0);
        }

        progressYear();
            
    }

    public void Update()
    {
        
    }

    public void Teleport(Room newRoom, Transform spawnPoint)
    {
        //ActivateRoom(newRoom);

        Transform targetSpawn =
            spawnPoint != null
            ? spawnPoint
            : newRoom.defaultSpawnPoint;

        player.transform.position = targetSpawn.position;
    }

    private void ActivateRoom(Room newRoom)
    {
        foreach (Room room in allRooms)
        {
            room.gameObject.SetActive(room == newRoom);
        }

        currentRoom = newRoom;
    }

    public void progressYear()
    {
        currentYear++;
        doorHandlerScript.switchDoor(currentYear-1);
    }
}