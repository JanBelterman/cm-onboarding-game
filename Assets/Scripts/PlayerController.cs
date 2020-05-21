using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public PlayerMovement playerMovement;

    private PlayerInputActions _inputAction;
    private Vector2 _movementInput;
    private void Awake() {
        // Get Unity's new Input System
        _inputAction = new PlayerInputActions();
        _inputAction.PlayerControls.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        playerMovement.MovePlayer(_movementInput);
        // transform.eulerAngles = new Vector3(0, transform.rotation.y, 0);
    }
    
    private void OnEnable() {
        _inputAction.Enable();
    }

    private void OnDisable() {
        _inputAction.Disable();
    }
}
