using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{

    public Text nameText;
    public Text dialogText;

    public Transform dialogBoxGUI;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public string Names;

    public string[] dialogLines;

    public bool letterIsMultiplied = false;
    public bool dialogActive = false;
    public bool dialogEnded = false;
    public bool outOfRange = true;

    public bool dialogControlClicked = false;
    private PlayerInputActions _inputAction;

    private void Awake()
    {
        dialogText.text = "";

        _inputAction = new PlayerInputActions();
        _inputAction.DialogControls.NextDialog.performed += ctx => dialogControlClicked = true;
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }

    public void EnterRangeOfNPC()
    {
        StartDialog();
        outOfRange = false;
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;
        if (!dialogActive)
        {
            dialogActive = true;
            StartCoroutine(StartDialog());
        }
        StartDialog();
    }

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

                    if (currentDialogIndex >= dialogLength)
                    {
                        dialogEnded = true;
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (dialogEnded == false && dialogControlClicked)
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
                    //if (Input.GetKey(DialogInput))
                    //{
                    // yield return new WaitForSeconds(letterDelay * letterMultiplier);

                    // if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    // }
                    //else
                    //{
                    yield return new WaitForSeconds(letterDelay);

                    //}
                }
                else
                {
                    dialogEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (dialogControlClicked)
                {
                    dialogControlClicked = false;
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
        }
    }
}
