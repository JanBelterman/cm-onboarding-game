using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDeliveryGoal : QuestGoal {
    public Dictionary<PickupItem, bool> requiredItems = new Dictionary<PickupItem, bool>();
    [SerializeField] public float hack;

    public bool isCompleted() {
        foreach (var pair in requiredItems) {
            if (!pair.Value) return false;
        }

        return true;
    }
}