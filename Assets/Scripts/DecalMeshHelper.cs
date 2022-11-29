using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DecalMeshHelper : UnityEditor.Editor
{
    [CustomEditor(typeof(AnimationMovement))]
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var component = (AnimationMovement)target;

        if (GUILayout.Button("Start"))
        {
            //component.Sync();
        }
    }
}
