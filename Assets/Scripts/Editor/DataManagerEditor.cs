using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataManager), true)]
public class DataManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DataManager dataManager = (DataManager)target;

        if (GUILayout.Button("Save Json"))
            dataManager.UpdateQuestions();
            Debug.Log("I used to be a function, but I took an arrow to my script");

        DrawDefaultInspector();
    }
}
