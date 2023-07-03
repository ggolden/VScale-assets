using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    public int range = 300;
    public int segmentLength = 10;
    public Object sebmentPrefab;

    public void LayTrack(GameObject container, bool withUndo)
    {
        // register an undo
        //Undo.RegisterFullObjectHierarchyUndo(container, "Generate Track");

        // clear
        for (int i = container.transform.childCount - 1; i >= 0; i--)
        {
            //DestroyImmediate(container.transform.GetChild(i).gameObject);
            if (withUndo) Undo.DestroyObjectImmediate(container.transform.GetChild(i).gameObject);
        }

        // lay
        for (int z = -range; z <= range; z += segmentLength)
        {
            GameObject segment = Segment(z, container);
            if (withUndo) Undo.RegisterCreatedObjectUndo(segment, "Generate Track");
        }
    }

    private GameObject Segment(int z, GameObject parent)
    {
        //Object prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
        GameObject segment = Instantiate(sebmentPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        segment.transform.position = new Vector3(0, 0, z);
        segment.transform.SetParent(parent.transform, false);
        segment.name = $"Track {z}";

        return segment;
    }

    // Start is called before the first frame update
    void Start()
    {
        LayTrack(gameObject, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
