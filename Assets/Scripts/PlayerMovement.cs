using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private bool isRelativeToCamera = false;

    private Rigidbody _rb;
    private Camera _mainCamera;

    public void Awake() {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }
    
    public void MovePlayer(Vector2 direction) {
        Vector3 movement;
        
        if (isRelativeToCamera) {
            // Get the camera's forward and right axis
            var forward = _mainCamera.transform.forward;
            var right = _mainCamera.transform.right;
            
            // Normalize vectors to horizontal plane
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 relDirection = forward * direction.y + right * direction.x;
            movement = new Vector3(relDirection.x, _rb.velocity.y, relDirection.z);
        } else {
            movement = new Vector3(direction.x, _rb.velocity.y, direction.y);
        }
        
        // Apply speed and deltatime
        movement = movement * (moveSpeed * Time.deltaTime);

        // Apply velocity
        _rb.velocity = movement;
    }
}