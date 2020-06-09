using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 50f;
    public float midAirMoveSpeed = 10f;
    public float rotationDamp = 0.15f;
    public float gravity = 20f;

    private CharacterController _controller;
    private PlayerInputActions _inputActions;
    private Vector2 _movementInput;
    private Vector3 _velocity;
    private Camera _camera;

    private void Awake() {
        _controller = GetComponent<CharacterController>();
        _inputActions = new PlayerInputActions();
        _inputActions.PlayerControls.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
        _camera = Camera.main;
    public SoundManager soundManager;
    }

    private void Update() {
        MovePlayer();
    }

    private void MovePlayer() {
        Vector3 movement = Vector3.zero;
        
        // Get the camera's forward and right axis
        var forward = _camera.transform.forward;
        var right = _camera.transform.right;
            
        // Normalize vectors to horizontal plane
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        movement = forward * _movementInput.y + right * _movementInput.x;
        
        // Check if the player is standing on the ground
        if (_controller.isGrounded) {
            _velocity = Vector3.zero;
            movement *= moveSpeed;
        } else {
            // Movement if the player is mid-air
            movement *= midAirMoveSpeed;
        }
        
        // Apply gravity
        _velocity.y += gravity * Time.deltaTime;
        movement.y = _velocity.y;

        LookAhead(movement);
        
        // Apply movement to CharacterController
        _controller.Move(movement * Time.deltaTime);
        anim.SetFloat("speed", (Mathf.Abs(direction.y) + Mathf.Abs(direction.x)));
        SoundManager.instance.PlayFootstep();
        if(Mathf.Abs(direction.y) + Mathf.Abs(direction.x) > 0) {

        }

    }
    
    private void LookAhead(Vector3 movementDir) {
        // Set input deadzone to ignore
        if (movementDir.magnitude > 1.5f) {
            Quaternion LookRotation = Quaternion.LookRotation(movementDir, Vector3.up);
            // Remove other rotations so it only rotates on Y
            Quaternion LookRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, LookRotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, LookRotationY, rotationDamp);
        }
    }
    
    private void OnEnable() {
        _inputActions.Enable();
    }

    private void OnDisable() {
        _inputActions.Disable();
    }
}
