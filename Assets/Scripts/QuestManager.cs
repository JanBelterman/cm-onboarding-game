using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    [HideInInspector] public bool acceptClicked = false;
    [HideInInspector] public bool declineClicked = false;
    [HideInInspector] public string enteredPuzzle = null;
    private UIManager _uiManager;
    private PickupController _pickupController;
    
    private void Awake() {
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
        _uiManager = FindObjectOfType<UIManager>();
        _pickupController = FindObjectOfType<PickupController>();
    }

    public IEnumerator AcceptQuest(Quest quest) {
        if (completedQuests.Contains(quest)) yield break;
        if (activeQuests.Contains(quest))  yield break;
        int requiredQuestCount = 0;
        foreach (QuestData requiredQuest in quest.QuestData.requiredQuests) {
            foreach (Quest completedQuest in completedQuests) {
                if (completedQuest.QuestData == requiredQuest) requiredQuestCount++;
            }
        }
        if (requiredQuestCount != quest.QuestData.requiredQuests.Length && quest.QuestData.requiredQuests.Length > 0)  
            yield break;

        _uiManager.ShowQuest(quest);
        
        while (!acceptClicked && !declineClicked) {
            yield return null;
        }
        if (declineClicked) {
            declineClicked = false;
            yield break;
        }
        acceptClicked = false;

        Debug.Log($"Quest: \"{quest.QuestData.title}\" added to active quests.");
        activeQuests.Add(quest);
    }

    public IEnumerator CompleteQuest(NPC completionNPC, PickupItem heldItem) {
        foreach (Quest questMatch in activeQuests.Where(quest => quest.deliveryNPC == completionNPC)) {
            if (questMatch.requiredItem == heldItem) {
                _uiManager.ShowPuzzle();

                
                while (!enteredPuzzle.Equals(questMatch.QuestData.puzzleAnswer.Trim())) {
                    while (String.IsNullOrEmpty(enteredPuzzle)) {
                        yield return null;
                    }

                    string test1 = "kut";
                    string test2 = "kut";
                    
                    Debug.Log(test1.Equals(test2));

                    Debug.Log(enteredPuzzle.Trim().ToString() + " --- " + questMatch.QuestData.puzzleAnswer.Trim().ToString());
                    Debug.Log(enteredPuzzle.GetType() + " --- " + questMatch.QuestData.puzzleAnswer.GetType());
                    Debug.Log(enteredPuzzle.Trim().Equals(questMatch.QuestData.puzzleAnswer.Trim()));
                    Debug.Log(string.Compare(enteredPuzzle.Trim(), questMatch.QuestData.puzzleAnswer.Trim()));
                    if (!enteredPuzzle.Equals(questMatch.QuestData.puzzleAnswer.Trim())) {
                        StartCoroutine(_uiManager.PuzzleIncorrect());
                        enteredPuzzle = String.Empty;
                    }
                } 
                
                enteredPuzzle = String.Empty;
                _uiManager.PuzzleCorrect();

                completedQuests.Add(questMatch);
                activeQuests.Remove(questMatch);
                _pickupController.Remove();
                Debug.Log($"Quest: \"{questMatch.QuestData.title}\" completed.");
            }
        }

        yield return null;
    }
}
