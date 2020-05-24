using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : MonoBehaviour {
	private PlayerInputActions _inputActions;
	private bool _carried = false;
	private GameObject _nearbyPlayer = null;
	
	
	void Awake() {
		_inputActions = new PlayerInputActions();
		_inputActions.PlayerControls.Interact.performed += Pickup;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			_nearbyPlayer = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			_nearbyPlayer = null;
		}
	}

	public void Pickup(InputAction.CallbackContext ctx) {
	    if (_nearbyPlayer!= null && !_carried) {
		    if (_nearbyPlayer.GetComponent<PlayerController>().Pickup(this)) {
			    _carried = true;
		    }
	    }
    }

    public void Drop() {
	    _carried = false;
    }
    
    private void OnEnable() {
	    _inputActions.Enable();
    }
    
    private void OnDisable() {
	    _inputActions.Disable();
    }
}

