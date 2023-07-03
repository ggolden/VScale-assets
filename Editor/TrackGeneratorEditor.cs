using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrackGenerator))]
public class TrackGeneratorEditor : Editor
{
    TrackGenerator trackGenerator;
    GameObject container;

    void OnEnable()
    {
        trackGenerator = (TrackGenerator)target;
        container = trackGenerator.gameObject;
    }

    public override void OnInspectorGUI()
    {
        if (DrawDefaultInspector())
        {
            // TODO:
        }

        if (GUILayout.Button("Generate"))
        {
            trackGenerator.LayTrack(container);
        }
    }
}
