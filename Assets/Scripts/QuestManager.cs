using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public Animator assistentAnimator;


    [HideInInspector] public bool acceptClicked = false;
    [HideInInspector] public bool declineClicked = false;
    [HideInInspector] public string enteredPuzzle = null;
    private UIManager _uiManager;
    private PickupController _pickupController;

    private void Awake()
    {
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
        _uiManager = FindObjectOfType<UIManager>();
        _pickupController = FindObjectOfType<PickupController>();
    }

    public IEnumerator AcceptQuest(Quest quest)
    {
        if (completedQuests.Contains(quest)) yield break;
        if (activeQuests.Contains(quest)) yield break;
        int requiredQuestCount = 0;
        foreach (QuestData requiredQuest in quest.QuestData.requiredQuests)
        {
            foreach (Quest completedQuest in completedQuests)
            {
                if (completedQuest.QuestData == requiredQuest) requiredQuestCount++;
            }
        }
        if (requiredQuestCount != quest.QuestData.requiredQuests.Length && quest.QuestData.requiredQuests.Length > 0)
            yield break;

        _uiManager.ShowQuest(quest);

        while (!acceptClicked && !declineClicked)
        {
            yield return null;
        }
        if (declineClicked)
        {
            declineClicked = false;
            yield break;
        }
        acceptClicked = false;

        Debug.Log($"Quest: \"{quest.QuestData.title}\" added to active quests.");
        activeQuests.Add(quest);
        _uiManager.showCurrentQuest(quest);
    }

    public IEnumerator CompleteQuest(NPC completionNPC, PickupItem heldItem)
    {
        List<Quest> tempCompletedQuests = new List<Quest>();
        foreach (Quest questMatch in activeQuests.Where(quest => quest.deliveryNPC == completionNPC))
        {
            if (questMatch.requiredItem == heldItem)
            {
                _uiManager.ShowPuzzle();

                while (!enteredPuzzle.Equals(questMatch.QuestData.puzzleAnswer.Trim()))
                {
                    while (String.IsNullOrEmpty(enteredPuzzle))
                    {
                        yield return null;
                    }

                    if (!enteredPuzzle.Equals(questMatch.QuestData.puzzleAnswer.Trim()))
                    {
                        StartCoroutine(_uiManager.PuzzleIncorrect());
                        enteredPuzzle = String.Empty;
                    }
                }
                if(questMatch.QuestData.accessToCEO == true) {
                    if (assistentAnimator != null) {
                    assistentAnimator.SetBool("AccessToCEO", true);
                    Debug.Log("Access status: " + assistentAnimator.GetBool("AccessToCEO"));
                    Debug.Log("Access granted to CEO");
                    }
                }

                enteredPuzzle = String.Empty;
                _uiManager.PuzzleCorrect();
                _uiManager.hideCurrentQuest();

                completedQuests.Add(questMatch);
                tempCompletedQuests.Add(questMatch);
                // activeQuests.Remove(questMatch);
                _pickupController.Remove();
                Debug.Log($"Quest: \"{questMatch.QuestData.title}\" completed.");
            }
        }

        foreach (Quest quest in tempCompletedQuests)
        {
            activeQuests.Remove(quest);
        }

        yield return null;
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
    }
}
