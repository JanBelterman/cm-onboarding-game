using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationDamp = 0.15f;
    [SerializeField] private bool isRelativeToCamera = false;

    private Rigidbody _rb;
    private Camera _mainCamera;
    public Animator anim;

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

            movement = forward * direction.y + right * direction.x;
        } else {
            movement = new Vector3(direction.x, 0f, direction.y);
        }
        
        // Apply speed and deltatime
        movement = movement * (moveSpeed * Time.deltaTime);
        movement.y = _rb.velocity.y;
        
        LookAhead(movement);

        // Apply velocity
        _rb.velocity = movement;

        anim.SetFloat("speed", (Mathf.Abs(direction.y) + Mathf.Abs(direction.x)));
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
}