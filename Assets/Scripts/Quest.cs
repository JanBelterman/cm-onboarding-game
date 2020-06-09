using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject {
    // public int questId;
    public string title;
    [TextArea]
    public string description;
}
