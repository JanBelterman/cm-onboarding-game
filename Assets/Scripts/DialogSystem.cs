using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{

    public Text nameText;
    public Text dialogText;

    public Transform dialogBoxGUI;
    public Transform dialogBoxTransform;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public string Names;

    public string[] dialogLines;

    public bool letterIsMultiplied = false;
    public bool dialogActive = false;
    public bool dialogEnded = false;
    public bool outOfRange = true;

    // public bool dialogControlClicked = false;
    private PlayerInputActions _inputAction;
    private NPC _currentNPC;

    private void Awake()
    {
        dialogText.text = "";

        _inputAction = new PlayerInputActions();
        // _inputAction.DialogControls.NextDialog.performed += ctx => dialogControlClicked = true;
    }

    private void Update() {
        if (_currentNPC != null) {
            var dialogPos = Camera.main.WorldToScreenPoint(_currentNPC.transform.position);
            dialogPos.y += 100f;
            dialogBoxTransform.position = dialogPos;
        }
    }

    public void EnterRangeOfNPC(NPC currentNPC) {
        _currentNPC = currentNPC;
        outOfRange = false;
        dialogBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;
        if (!dialogActive)
        {
            dialogActive = true;
            StartCoroutine(StartDialog());
        }
    }

    // public void NPCName()
    // {
    //     outOfRange = false;
    //     dialogBoxGUI.gameObject.SetActive(true);
    //     nameText.text = Names;
    //     if (!dialogActive)
    //     {
    //         dialogActive = true;
    //         StartCoroutine(StartDialog());
    //     }
    // }

    private IEnumerator StartDialog()
    {
        if (outOfRange == false)
        {
            int dialogLength = dialogLines.Length;
            int currentDialogIndex = 0;

            while (currentDialogIndex < dialogLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogLines[currentDialogIndex++]));

                    if (currentDialogIndex >= dialogLength) {
                        if (_currentNPC.TryGetComponent(out QuestGiver questGiver)) {
                            StartCoroutine(questGiver.GiveQuest());
                        }
                        dialogEnded = true;
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (dialogEnded == false && _inputAction.DialogControls.NextDialog.triggered)
                {
                    break;
                }
                yield return 0;
            }
            dialogEnded = false;
            dialogActive = false;
            DropDialog();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {

        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    yield return new WaitForSeconds(letterDelay);
                }
                else
                {
                    dialogEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (_inputAction.DialogControls.NextDialog.triggered)
                {
                    // dialogControlClicked = false;
                    break;
                }
                yield return 0;
            }
            dialogEnded = false;
            letterIsMultiplied = false;
            dialogText.text = "";
        }
    }

    public void DropDialog()
    {
        dialogBoxGUI.gameObject.SetActive(false);
        _currentNPC = null;
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogActive = false;
            StopAllCoroutines();
            dialogBoxGUI.gameObject.SetActive(false);
            _currentNPC = null;
        }
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }
}
