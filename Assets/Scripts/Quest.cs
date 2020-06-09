using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject {
    public string title;
    [TextArea]
    public string description;
    public Quest[] requiredQuests;
    [SerializeReference]
    public QuestGoal[] questGoals;
}
