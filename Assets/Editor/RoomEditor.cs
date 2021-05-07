using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (Room)target;

        if (GUILayout.Button("Setup Doors", GUILayout.Height(40)))
        {
            script.SetupDoors();
        }

    }
}