using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
    using UnityEditor;
#endif

using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    public int range = 300;
    public int segmentLength = 10;
    public Object segmentPrefab;

    public void GenerateTrack(GameObject container, bool withUndo)
    {
        // clear
        for (int i = container.transform.childCount - 1; i >= 0; i--)
        {
            #if UNITY_EDITOR
               if (withUndo) {
                    Undo.DestroyObjectImmediate(container.transform.GetChild(i).gameObject);
               } else {
                    DestroyImmediate(container.transform.GetChild(i).gameObject);
               }
            #else
                DestroyImmediate(container.transform.GetChild(i).gameObject);
            #endif
        }

        // generate
        for (int z = -range; z <= range; z += segmentLength)
        {
            GameObject segment = Segment(z, container);
            #if UNITY_EDITOR
                if (withUndo) Undo.RegisterCreatedObjectUndo(segment, "Generate Track");
            #endif
        }
    }

    private GameObject Segment(int z, GameObject parent)
    {
        GameObject segment = Instantiate(segmentPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        segment.transform.position = new Vector3(0, 0, z);
        segment.transform.SetParent(parent.transform, false);
        segment.name = $"Track {z}";

        return segment;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateTrack(gameObject, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
