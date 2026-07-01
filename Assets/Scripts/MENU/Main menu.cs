using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Play();
        }
    }
}