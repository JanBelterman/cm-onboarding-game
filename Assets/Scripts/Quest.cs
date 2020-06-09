using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject {
    public string title;
    [TextArea] public string description;
    public Quest[] requiredQuests;

    [SerializeReference] public List<IQuestGoal> questGoal;

    public void AddDeliveryGoal() {
        questGoal.Add(new ItemDeliveryGoal());
    }

    public void AddTalkGoal() {
        questGoal.Add(new TalkGoal());
    }
}
