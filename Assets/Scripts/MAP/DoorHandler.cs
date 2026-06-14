using UnityEngine;

public class DoorHandler : MonoBehaviour
{

    public Transform[] doors;
    //public Transform door_an2;
    //public Transform door_an3;

    private int enabled_door = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void switchDoor(int door)
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if (door == i)
            {
                enabled_door = i;
                doors[i].gameObject.SetActive(true);
            }
            else
            {
                doors[i].gameObject.SetActive(false);
            }
        }

    }
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
