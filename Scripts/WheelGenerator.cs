using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGenerator : MonoBehaviour
{
    public float radius = 0.45f;
    public float flangeRadiusBump = 0.065f;
    public int divisions = 64;
    public float tread = 0.1f;
    public float flangeWidth = 0.03f;
    public Vector3 center = new Vector3(0, 0, 0);
    public Plane plane = Plane.ZY;
    public Facing facing = Facing.XPlus;
    public float slope = 5;

    public int[] triangles;
    public Vector3[] verticies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddBridges(int[] xMinus, int[] xPlus, int divisions, MeshBuilder meshBuilder) {
        for (int i = 1; i < divisions; i++)
        {
            meshBuilder.addFace4i(new int[] { xPlus[i], xPlus[i + 1], xMinus[i + 1], xMinus[i] });
        }
        meshBuilder.addFace4i(new int[] { xPlus[divisions], xPlus[1], xMinus[1], xMinus[divisions] });
    }

    private void Unit(Vector3 at, float w, float rs, float r, int d, MeshBuilder meshBuilder) {
        int[] xMinus = meshBuilder.addFaceCircle(
            r - rs/2,
            d,
            at - new Vector3(w/2, 0, 0),
            plane,
            Facing.XMinus
        );

        int[] xPlus = meshBuilder.addFaceCircle(
            r + rs/2,
            divisions,
            at + new Vector3(w/2, 0, 0),
            plane,
            Facing.XPlus
        );

        AddBridges(xMinus, xPlus, divisions, meshBuilder);
        //for (int i = 1; i < divisions; i++)
        //{
        //    meshBuilder.addFace4i(new int[] { xPlus[i], xPlus[i + 1], xMinus[i + 1], xMinus[i] });
        //}
        //meshBuilder.addFace4i(new int[] { xPlus[divisions], xPlus[1], xMinus[1], xMinus[divisions] });
    }

    public void Generate(GameObject container) {
        //Debug.Log("Generate");
        MeshBuilder meshBuilder = new MeshBuilder();

        // slope as radius +-: difference from target radius +-
        float rs = slope == 0 ? 0 : tread / slope;

        // wheel
        Unit(
            center,
            tread,
            rs,
            radius,
            divisions,
            meshBuilder
        );

        // flange
        Unit(
            center + new Vector3(tread/2 + flangeWidth/2, 0, 0),
            flangeWidth,
            0f,
            radius + flangeRadiusBump,
            divisions,
            meshBuilder
        );

        //int[] xMinus = meshBuilder.addFaceCircle(radius - (tread / slope), divisions, center + new Vector3(-tread/2, 0, 0), plane, Facing.XMinus);
        //int[] xPlus = meshBuilder.addFaceCircle(radius, divisions, center + new Vector3(tread/2 ,0, 0), plane, Facing.XPlus);

        //for (int i = 1; i < divisions; i++) {
        //    meshBuilder.addFace4i(new int[] { xPlus[i], xPlus[i+1], xMinus[i+1], xMinus[i] });
        //}
        //meshBuilder.addFace4i(new int[] { xPlus[divisions], xPlus[1], xMinus[1], xMinus[divisions] });

        //meshBuilder.addFaceCircle(radius, divisions, center, plane, facing);

        //meshBuilder.addFaceCircle(radius, divisions, new Vector3() center);
        //meshBuilder.addFace3(new Vector3[]{
        //    new Vector3(0, 0, 1),
        //    new Vector3(1, 1, 0),
        //    new Vector3(1, 0, 0)
        //});

        //meshBuilder.addFace4(new Vector3[]{
        //    new Vector3(0, 0, 0) + Vector3.right,
        //    new Vector3(0, 1, 0) + Vector3.right,
        //    new Vector3(1, 1, 0) + Vector3.right,
        //    new Vector3(1, 0, 1) + Vector3.right
        //});

        // meshBuilder.addVertex(new Vector3(0, 0, 0));
        // meshBuilder.addVertex(new Vector3(1, 1, 0));
        // meshBuilder.addVertex(new Vector3(1, 0, 0));
        // meshBuilder.addTriangle(0, 1, 2);

        MeshFilter meshFilter = container.GetComponent<MeshFilter>();
        meshFilter.mesh = meshBuilder.CreateMesh();

        triangles = meshBuilder.getTriangles();
        verticies = meshBuilder.getVerticies();
    }
}
