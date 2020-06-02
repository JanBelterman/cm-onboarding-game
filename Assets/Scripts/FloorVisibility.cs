using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorVisibility : MonoBehaviour {
    public GameObject[] invisibleFloors;

    // Player enters floor
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // Activate all children (make floor visible)
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
            
            // Deactivate all children of invisible floors
            if (invisibleFloors.Length > 0) {
                foreach (GameObject floor in invisibleFloors) {
                    foreach (Transform child in floor.transform) {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
