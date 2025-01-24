using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


public class SG_InputController : MonoBehaviour
{
    //In-Game Inputs!!
    public static Vector2 LookInput { get; private set; }
    public static bool IsAiming { get; private set; }

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();

        // Register for actions
        inputActions.Player.Look.performed += OnLookPerformed;
        inputActions.Player.Aim.performed += OnAimButtonDown;
        inputActions.Player.Aim.canceled += OnAimButtonUp;
    }

    private void Update()
    {
        LookInput = inputActions.Player.Look.ReadValue<Vector2>();
    }

    private void OnLookPerformed(CallbackContext context)
    {

    }

    private void OnAimButtonDown(CallbackContext context)
    {
        IsAiming = true;
    }

    private void OnAimButtonUp(CallbackContext context)
    {
        IsAiming = false;
    }

    private void OnDestroy()
    {
        if (inputActions != null)
            inputActions.Player.Disable();
    }
}
