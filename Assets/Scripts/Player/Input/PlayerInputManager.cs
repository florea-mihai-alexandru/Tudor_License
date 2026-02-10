using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private Vector2 moveInput;
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Attack");
        }
        if (context.performed)
        {
            Debug.Log("HELD");
        }
        if (context.canceled)
        {
            Debug.Log("RELEASE");
        }
    }
        
}

