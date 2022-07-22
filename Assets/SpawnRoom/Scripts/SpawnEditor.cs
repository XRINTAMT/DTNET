using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SpawnRoom))]

public class SpawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SpawnRoom spawnRoom = (SpawnRoom)target;

        if (GUILayout.Button("Create room")) spawnRoom.Spawn();
    }
}
