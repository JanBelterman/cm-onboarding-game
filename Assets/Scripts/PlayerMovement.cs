using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    private CharacterController _controller;

    private void Awake() {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MovePlayer() {
        
    }
}
