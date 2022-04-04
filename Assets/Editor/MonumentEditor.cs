using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonumentMaker))]
public class MonumentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MonumentMaker maker = target as MonumentMaker;
        maker.Init();
    }

}
