using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupController : MonoBehaviour {
    private PlayerInputActions _inputActions;
    private PickupItem _heldItem = null;
    
    private void Awake() {
        // Get Unity's new Input System
        _inputActions = new PlayerInputActions();
        _inputActions.PlayerControls.Drop.performed += Drop;
    }

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
    
    
