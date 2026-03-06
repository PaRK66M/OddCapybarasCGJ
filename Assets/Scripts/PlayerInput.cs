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

    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActionMap();
        EnableInput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableInput()
    {
        _playerInputActions.Movement.Enable();
        _playerInputActions.Actions.Enable();

        _playerInputActions.Movement.Walking.performed += playerMovement.UpdateMovementInput;
        _playerInputActions.Movement.Walking.canceled += playerMovement.UpdateMovementInput;
        _playerInputActions.Movement.Turning.performed += playerMovement.UpdateTurningInput;
        _playerInputActions.Movement.Turning.canceled += playerMovement.UpdateTurningInput;

        _playerInputActions.Actions.SwapWorld.performed += worldManager.SwapWorldInput;
    }
}
