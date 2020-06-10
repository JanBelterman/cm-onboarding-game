using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    [Header("Quest Accept Screen")]
    public GameObject questAcceptContainer;
    public TextMeshProUGUI questAcceptTitle;
    public TextMeshProUGUI questAcceptDescription;

    [Header("Quest Puzzle Screen")] 
    public GameObject puzzleScreen;
    public TextMeshProUGUI inputField;
    public GameObject feedbackText;

    private QuestManager _questManager;
    private PlayerInputActions _inputActions;

    private void Awake() {
        _questManager = FindObjectOfType<QuestManager>();
        _inputActions = new PlayerInputActions();
    }

    public void ShowQuest(Quest quest) {
        questAcceptTitle.text = quest.QuestData.title;
        questAcceptDescription.text = quest.QuestData.description;
        questAcceptContainer.SetActive(true);
    }

    public void ShowPuzzle() {
        puzzleScreen.SetActive(true);
    }

    public void OnQuestAccept() {
        _questManager.acceptClicked = true;
    }
    
    public void OnQuestDecline() {
        _questManager.declineClicked = true;
    }

    public void OnPuzzleSubmit() {
        _questManager.enteredPuzzle = inputField.text;
    }

    public void PuzzleCorrect() {
        inputField.text = string.Empty;
        puzzleScreen.SetActive(false);
    }

    public IEnumerator PuzzleIncorrect() {
        inputField.text = String.Empty;
        
        feedbackText.SetActive(true);
        yield return new WaitForSeconds(3);
        feedbackText.SetActive(false);
    }
}
