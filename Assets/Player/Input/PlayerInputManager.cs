using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool InteractionInput {  get; private set; }

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

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
            DashInput = true;
    }

    public void DashUsed() => DashInput = false;

}

