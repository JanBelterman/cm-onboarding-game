using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupController : MonoBehaviour {
    public PickupItem heldItem = null;
    
    private PlayerInputActions _inputActions;
    private Animator _animator;
    
    private void Awake() {
        // Get Unity's new Input System
        _inputActions = new PlayerInputActions();
        _inputActions.PlayerControls.Drop.performed += Drop;
        _animator = GetComponentInChildren<Animator>();
    }

    public bool Pickup(PickupItem item) {
        if (heldItem == null) {
            heldItem = item;
            heldItem.gameObject.transform.SetParent(this.transform);
            heldItem.transform.localPosition = new Vector3(0f, 0f, 1f);
            heldItem.transform.rotation = Quaternion.identity;
            _animator.SetBool("hasObject", true);
            return true;
        }

        return false;
    }

    public void Drop(InputAction.CallbackContext ctx) {
        if (heldItem != null) {
            heldItem.Drop();
            heldItem.transform.parent = null;
            heldItem = null;
            _animator.SetBool("hasObject", false);
        }
    }

    public void Remove() {
        if (heldItem != null) {
            Destroy(heldItem.gameObject);
            heldItem = null;
            _animator.SetBool("hasObject", false);
        }
    }
    
    private void OnEnable() {
        _inputActions.Enable();
    }

    private void OnDisable() {
        _inputActions.Disable();
    }
}
    
    
