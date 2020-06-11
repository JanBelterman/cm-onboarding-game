using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Quest Accept Screen")]
    public GameObject questAcceptContainer;
    public TextMeshProUGUI questAcceptTitle;
    public TextMeshProUGUI questAcceptDescription;

    [Header("Quest Puzzle Screen")]
    public GameObject puzzleScreen;
    public TMP_InputField inputField;
    public GameObject feedbackText;

    [Header("Quest HUD")]
    public GameObject questHud;
    public TextMeshProUGUI currentQuestTitle;
    public TextMeshProUGUI currentQuestDescription;

    [Header("Completed quest HUD")]
    public GameObject completedQuest;
    public TextMeshProUGUI completedQuestTitle;

    private QuestManager _questManager;
    private PlayerInputActions _inputActions;
    private SoundManager _audioManager;

    private void Awake()
    {
        _questManager = FindObjectOfType<QuestManager>();
        _inputActions = new PlayerInputActions();
        _audioManager = FindObjectOfType<SoundManager>();
    }

    public void ShowQuest(Quest quest)
    {
        questAcceptTitle.text = quest.QuestData.title;
        questAcceptDescription.text = quest.QuestData.description;
        questAcceptContainer.SetActive(true);
    }

    public void showCurrentQuest(Quest quest)
    {
        currentQuestTitle.text = quest.QuestData.title;
        currentQuestDescription.text = quest.QuestData.description;
        questHud.SetActive(true);
    }

    public void hideCurrentQuest()
    {
        questHud.SetActive(false);
        completedQuest.SetActive(true);
        completedQuestTitle.text = currentQuestTitle.text;
        StartCoroutine(showQuestCompleted());
    }

    public IEnumerator showQuestCompleted()
    {
        _audioManager.PlayQuestCompleteSound();
        yield return new WaitForSeconds(3);
        completedQuest.SetActive(false);
    }

    public void ShowPuzzle()
    {
        puzzleScreen.SetActive(true);
    }

    public void OnQuestAccept()
    {
        _questManager.acceptClicked = true;
    }

    public void OnQuestDecline()
    {
        _questManager.declineClicked = true;
    }

    public void OnPuzzleSubmit()
    {
        _questManager.enteredPuzzle = inputField.text;
    }

    public void OnPuzzleClose()
    {
        puzzleScreen.SetActive(false);
        _questManager.StopCoroutines();
    }

    public void PuzzleCorrect()
    {
        inputField.text = string.Empty;
        puzzleScreen.SetActive(false);
    }

    public IEnumerator PuzzleIncorrect()
    {
        inputField.text = String.Empty;

        feedbackText.SetActive(true);
        yield return new WaitForSeconds(3);
        feedbackText.SetActive(false);
    }
}
