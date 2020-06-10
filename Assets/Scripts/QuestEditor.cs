using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// [CustomEditor(typeof(Quest))]
public class QuestEditor : Editor {
    public string[] options = new[] {"Delivery Goal", "Talk Goal"};
    public int index = 0;

    private Quest _script;
    
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        // _script = (Quest)target;

        GUILayout.BeginHorizontal();
        
        EditorGUILayout.PrefixLabel("Add Quest Goal:");

        index = EditorGUILayout.Popup(index, options);
        if (GUILayout.Button("+")) {
            // AddQuestGoal();
        }
        
        GUILayout.EndHorizontal();
    }

    // private void AddQuestGoal() {
    //     switch (index) {
    //         case 0:
    //             _script.AddDeliveryGoal();
    //             break;
    //         case 1:
    //             _script.AddTalkGoal();
    //             break;
    //         default:
    //             Debug.LogError("Unrecognized Option");
    //             break;
    //     }
    // }
}
