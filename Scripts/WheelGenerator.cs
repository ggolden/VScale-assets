using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(GameObject container) {
        Debug.Log("Generate");
        MeshBuilder meshBuilder = new MeshBuilder();
        meshBuilder.addFace3(new Vector3[]{
            new Vector3(0, 0, 1),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0)
        });

        meshBuilder.addFace4(new Vector3[]{
            new Vector3(0, 0, 0) + Vector3.right,
            new Vector3(0, 1, 0) + Vector3.right,
            new Vector3(1, 1, 0) + Vector3.right,
            new Vector3(1, 0, 1) + Vector3.right
        });

        // meshBuilder.addVertex(new Vector3(0, 0, 0));
        // meshBuilder.addVertex(new Vector3(1, 1, 0));
        // meshBuilder.addVertex(new Vector3(1, 0, 0));
        // meshBuilder.addTriangle(0, 1, 2);

        MeshFilter meshFilter = container.GetComponent<MeshFilter>();
        meshFilter.mesh = meshBuilder.CreateMesh();
    }
}
