using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestionManager), true)]
public class QuestionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        QuestionManager qManager = (QuestionManager)target;

        if (GUILayout.Button("Start Questioning"))
            qManager.TriggerQuestion();

        DrawDefaultInspector();
    }
}
