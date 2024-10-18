using System;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    public float Movement => playerControls.Gameplay.Movement.ReadValue<float>();

    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnSlide;
    public event Action<bool> OnCrouch;  // Evento para agachar com true (iniciar) e false (parar)

    public InputManager()
    {
        playerControls = new PlayerControls();
        EnablePlayerInput();

        playerControls.Gameplay.Jump.performed += OnJumpPerformed;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
        playerControls.Gameplay.Slide.performed += OnSlidePerformed;

        // Agachar (pressionado ou solto)
        playerControls.Gameplay.Crouch.started += OnCrouchStarted;   // Quando começar a agachar
        playerControls.Gameplay.Crouch.canceled += OnCrouchCanceled; // Quando parar de agachar
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    private void OnSlidePerformed(InputAction.CallbackContext context)
    {
        OnSlide?.Invoke();
    }

    private void OnCrouchStarted(InputAction.CallbackContext context)
    {
        OnCrouch?.Invoke(true);  // Inicia o agachamento
    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        OnCrouch?.Invoke(false);  // Encerra o agachamento
    }

    public void DisablePlayerInput() => playerControls.Gameplay.Disable();

    public void EnablePlayerInput() => playerControls.Gameplay.Enable();
}