using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDeliveryGoal : IQuestGoal {
    [Header("Item Delivery Goal")]
    public float test;
    
    public Dictionary<PickupItem, bool> requiredItems = new Dictionary<PickupItem, bool>();

    public bool isCompleted() {
        foreach (var pair in requiredItems) {
            if (!pair.Value) return false;
        }

        return true;
    }
}