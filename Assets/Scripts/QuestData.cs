using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestData : ScriptableObject {
    public string title;
    [TextArea] public string description;
    public string puzzleAnswer;
    public QuestData[] requiredQuests;

    // [SerializeReference] public List<IQuestGoal> questGoal;
    //
    // public void AddDeliveryGoal() {
    //     questGoal.Add(new ItemDeliveryGoal());
    // }
    //
    // public void AddTalkGoal() {
    //     questGoal.Add(new TalkGoal());
    // }
}
