using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothMoveSpeed = 0.125f;
    public float smoothRotationSpeed = 0.125f;

    private Vector3 offset;
    private Vector3 velocity;

    public void Awake() {
        // Calculate offset from player 
        offset = new Vector3(transform.position.x - target.position.x,
            transform.position.y - target.position.y,
            transform.position.z - target.position.z);
    }

    public void FixedUpdate() {
        // Set desired position and rotation
        Vector3 desiredPos = target.position + offset;
        Quaternion desiredRot = Quaternion.LookRotation(target.transform.position - transform.position);

        // Smooth towards desired position
        Vector3 smoothedPos = Vector3.SmoothDamp(transform.position,
            desiredPos,
            ref velocity,
            smoothMoveSpeed * Time.deltaTime);
        transform.position = smoothedPos;
        
        // Smooth towards desired rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            desiredRot, 
            smoothRotationSpeed * Time.deltaTime);
    }
}
