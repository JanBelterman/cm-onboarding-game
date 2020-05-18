using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public PlayerMovement _playerMovement;

    private PlayerInputActions _inputAction;
    private Vector2 _movementInput;
    private void Awake() {
        // Get Unity's new Input System
        _inputAction = new PlayerInputActions();
        _inputAction.PlayerControls.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        _playerMovement.MovePlayer(_movementInput);
    }
    
    private void OnEnable() {
        _inputAction.Enable();
    }

    private void OnDisable() {
        _inputAction.Disable();
    }
}
