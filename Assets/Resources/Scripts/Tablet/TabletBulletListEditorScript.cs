#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TabletBulletListMaker))]
public class TabletBulletListEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TabletBulletListMaker BulletListSettings = (TabletBulletListMaker)target;
        if (GUI.changed) BulletListSettings.FormList();
    }
}
#endif
