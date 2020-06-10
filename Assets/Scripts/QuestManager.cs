using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    private void Awake() {
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
    }

    public IEnumerator AcceptQuest(Quest quest) {
        if (completedQuests.Contains(quest)) yield return null;
        if (activeQuests.Contains(quest))  yield return null;
        int requiredQuestCount = 0;
        foreach (QuestData requiredQuest in quest.QuestData.requiredQuests) {
            foreach (Quest completedQuest in completedQuests) {
                if (completedQuest.QuestData == requiredQuest) requiredQuestCount++;
            }
        }
        if (requiredQuestCount != quest.QuestData.requiredQuests.Length)  yield return null;

        // TODO: Show quest window
        
        // while (true) {
        //     
        // }
        
        Debug.Log($"Quest: \"{quest.QuestData.title}\" added to active quests.");
        activeQuests.Add(quest);
        yield return null;
    }

    public bool CompleteQuest(NPC completionNPC, PickupItem heldItem) {
        foreach (Quest questMatch in activeQuests.Where(quest => quest.deliveryNPC == completionNPC)) {
            if (questMatch.requiredItem == heldItem) {
                completedQuests.Add(questMatch);
                activeQuests.Remove(questMatch);
                Debug.Log($"Quest: \"{questMatch.QuestData.title}\" completed.");
                return true;
            } else {
                return false;
            }
        }

        return false;
    }
}
