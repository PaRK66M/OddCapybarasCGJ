using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
    // Objects
    private PlayerInputActionMap _playerInputActions;

    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private WorldManager worldManager;
    [SerializeField]
    private GameManager gameManager;

    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActionMap();
        EnableInput();
    }

    public void DisableInput(bool isPaused = false)
    {
        _playerInputActions.Movement.Disable();
        if (!isPaused)
        {
            _playerInputActions.Actions.Disable();
            _playerInputActions.Actions.PauseGame.performed -= gameManager.PauseGameInput;
        }

        _playerInputActions.Movement.Walking.performed -= playerMovement.UpdateMovementInput;
        _playerInputActions.Movement.Walking.canceled -= playerMovement.UpdateMovementInput;
        _playerInputActions.Movement.Turning.performed -= playerMovement.UpdateTurningInput;
        _playerInputActions.Movement.Turning.canceled -= playerMovement.UpdateTurningInput;
        _playerInputActions.Movement.Jump.performed -= playerMovement.UpdateJumpingInput;
        _playerInputActions.Movement.Jump.canceled -= playerMovement.UpdateJumpingInput;

        _playerInputActions.Actions.SwapWorld.performed -= worldManager.SwapWorldInput;

        
    }

    public void EnableInput(bool isPaused = false)
    {
        _playerInputActions.Movement.Enable();
        if (!isPaused)
        {
            _playerInputActions.Actions.Enable();
            _playerInputActions.Actions.PauseGame.performed += gameManager.PauseGameInput;
        }

        _playerInputActions.Movement.Walking.performed += playerMovement.UpdateMovementInput;
        _playerInputActions.Movement.Walking.canceled += playerMovement.UpdateMovementInput;
        _playerInputActions.Movement.Turning.performed += playerMovement.UpdateTurningInput;
        _playerInputActions.Movement.Turning.canceled += playerMovement.UpdateTurningInput;
        _playerInputActions.Movement.Jump.performed += playerMovement.UpdateJumpingInput;
        _playerInputActions.Movement.Jump.canceled += playerMovement.UpdateJumpingInput;

        _playerInputActions.Actions.SwapWorld.performed += worldManager.SwapWorldInput;

        
    }
}
