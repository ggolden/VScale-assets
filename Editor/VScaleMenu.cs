using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VScaleMenu : MonoBehaviour
{
    public static int range = 300;
    public static int segmentLength = 10;
    public static string assetPath = "Assets/VScaleAssets/Prefabs/track_10m_a.prefab";
    public static Vector3 trackPosition = Vector3.zero;

    [MenuItem("VScale/Lay Track")]
    public static void LayTrack()
    {
        GameObject track = new GameObject("Track");
        track.transform.position = trackPosition;

        Undo.RegisterCreatedObjectUndo(track, "Lay Track");

        for (int z = -range; z <= range; z += segmentLength) {
            GameObject segment = Segment(z, track);
        }
    }

    static GameObject Segment(int z, GameObject parent) {
        Object prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
        GameObject segment = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        segment.transform.position = new Vector3(0, 0, z);
        segment.transform.SetParent(parent.transform, false);
        segment.name = $"Track {z}";

        return segment;
    }
}
