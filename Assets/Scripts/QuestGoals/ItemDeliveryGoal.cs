using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDeliveryGoal : IQuestGoal {
    [Header("Item Delivery Goal")]
    public NPC deliveryNPC;
    public List<DeliveryItem> requiredItems;

    public bool isCompleted() {
        foreach (var deliveryItem in requiredItems) {
            if (!deliveryItem.delivered) return false;
        }

        return true;
    }
}