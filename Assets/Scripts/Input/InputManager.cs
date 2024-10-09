using UnityEngine.InputSystem;
using System;

public class InputManager
{
    private PlayerControls playerControls;

    public float Movement => playerControls.Gameplay.Movement.ReadValue<float>();

    public event Action OnJump;
    public event Action OnAttack;

    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Jump.performed += OnJumpPerformed;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }
}