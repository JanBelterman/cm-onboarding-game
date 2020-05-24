using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public PlayerMovement playerMovement;

    private PlayerInputActions _inputActions;
    private Vector2 _movementInput;
    public PickupItem _heldItem = null;
    
    private void Awake() {
        // Get Unity's new Input System
        _inputActions = new PlayerInputActions();
        _inputActions.PlayerControls.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
        _inputActions.PlayerControls.Drop.performed += Drop;
    }

    private void FixedUpdate() {
        playerMovement.MovePlayer(_movementInput);
    }

    // Pickup function. Returns true if item was picked up
    public bool Pickup(PickupItem item) {
        if (_heldItem == null) {
            _heldItem = item;
            _heldItem.gameObject.transform.SetParent(this.transform);
            _heldItem.transform.localPosition = new Vector3(0f, 0f, 1f);
            _heldItem.transform.rotation = Quaternion.identity;
            return true;
        }

        return false;
    }

    public void Drop(InputAction.CallbackContext ctx) {
        if (_heldItem != null) {
            _heldItem.Drop();
            _heldItem.transform.parent = null;
            _heldItem = null;
        }
    }
    
    private void OnEnable() {
        _inputActions.Enable();
    }

    private void OnDisable() {
        _inputActions.Disable();
    }
}
