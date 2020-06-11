using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour {
    public List<Quest> availableQuests;

    private QuestManager _questManager;
    
    private void Awake() {
        _questManager = FindObjectOfType<QuestManager>();
    }

    public IEnumerator GiveQuest() {
        foreach (Quest availableQuest in availableQuests) {
            yield return StartCoroutine(_questManager.AcceptQuest(availableQuest));
        }
    }
}
