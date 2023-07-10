using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WheelGenerator))]
public class WheelGeneratorEditor : Editor
{
    WheelGenerator wheelGenerator;
    GameObject container;

    void OnEnable()
    {
        wheelGenerator = (WheelGenerator)target;
        container = wheelGenerator.gameObject;
    }

    public override void OnInspectorGUI()
    {
        if (DrawDefaultInspector())
        {
            // TODO:
        }

        if (GUILayout.Button("Generate"))
        {
            wheelGenerator.Generate(container);
        }
    }
}
