using System;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public Transform chatObject;
    [Header("NPC Data")]
    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;
    
    private DialogSystem _dialogSystem;
    private PlayerInputActions _inputActions;

    void Awake() {
        _dialogSystem = FindObjectOfType<DialogSystem>();
        _inputActions = new PlayerInputActions();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _inputActions.DialogControls.NextDialog.triggered) {
            // Check if player is completing quest
            var pickupCont = other.GetComponent<PickupController>();
            var heldItem = pickupCont.heldItem;
            StartCoroutine(other.GetComponent<QuestManager>().CompleteQuest(this, heldItem));
            
            _dialogSystem.Names = Name;
            _dialogSystem.dialogLines = sentences;
            FindObjectOfType<DialogSystem>().EnterRangeOfNPC(this);
        }
    }

    public void OnTriggerExit() {
        FindObjectOfType<DialogSystem>().OutOfRange();
    }

    private void OnEnable() {
        _inputActions.Enable();
    }

    private void OnDisable() {
        _inputActions.Disable();
    }
}

