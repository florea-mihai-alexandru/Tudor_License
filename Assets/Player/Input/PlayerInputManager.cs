using System;
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
    public bool BlockInput { get; private set; }

    public bool LeftWeaponSwitch { get; private set; }
    public bool RightWeaponSwitch { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        //Debug.Log(MoveInput);
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
            AttackInput = true;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
            DashInput = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            DialogueManager.Instance.HandleEnterPress();
    public void OnWeaponLeftInput(InputAction.CallbackContext context)
    {
        if (context.started){
            LeftWeaponSwitch = true;
            //Debug.Log("LEFT");
        }

    }

    public void OnWeaponRightInput(InputAction.CallbackContext context)
    {
        if (context.started)
            RightWeaponSwitch = true;
    }

    public void DashUsed() => DashInput = false;
    public void AttackUsed() => AttackInput = false;
    public void LeftWeaponUsed() => LeftWeaponSwitch = false;
    public void RightWeaponUsed() => RightWeaponSwitch = false;


    }
}

