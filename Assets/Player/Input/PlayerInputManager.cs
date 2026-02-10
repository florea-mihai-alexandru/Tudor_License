using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        //Debug.Log(MoveInput);
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        //if (context.started)
        //{
        //    Debug.Log("Attack");
        //}
        //if (context.performed)
        //{
        //    Debug.Log("HELD");
        //}
        //if (context.canceled)
        //{
        //    Debug.Log("RELEASE");
        //}
    }
        
}

