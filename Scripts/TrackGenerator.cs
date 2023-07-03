using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    public int range = 300;
    public int segmentLength = 10;
    public string assetPath = "Assets/VScaleAssets/Prefabs/track_10m_a.prefab";
    public Object prefab;
    public Vector3 trackPosition = Vector3.zero;

    public void LayTrack(GameObject container)
    {
        for (int i = container.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(container.transform.GetChild(i).gameObject);
        }

        container.transform.position = trackPosition;

        for (int z = -range; z <= range; z += segmentLength)
        {
            GameObject segment = Segment(z, container);
        }

        Undo.RegisterCompleteObjectUndo(container, "Generate Track");
    }

    private GameObject Segment(int z, GameObject parent)
    {
        //Object prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
        GameObject segment = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        segment.transform.position = new Vector3(0, 0, z);
        segment.transform.SetParent(parent.transform, false);
        segment.name = $"Track {z}";

        return segment;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
