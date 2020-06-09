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

    public bool AcceptQuest(Quest quest) {
        if (completedQuests.Contains(quest)) return false;
        if (activeQuests.Contains(quest)) return false;
        foreach (Quest requiredQuest in quest.requiredQuests) {
            if (!completedQuests.Contains(requiredQuest)) return false;
        }
        
        Debug.Log($"Quest: \"{quest.title}\" added to active quests.");
        activeQuests.Add(quest);
        return true;
    }

    public bool CompleteQuest(Quest quest) {
        // TODO
        return false;
    }
}
