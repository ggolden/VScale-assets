// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Plane
{
    XY,
    ZY
}

public enum Facing
{
    XPlus,
    XMinus
}

public class MeshBuilder
{
    List<Vector3> verticies = new List<Vector3>();
    List<int> triangles = new List<int>();

    public Vector3[] getVerticies() {
        return verticies.ToArray();
    }

    public int[] getTriangles() {
        return triangles.ToArray();
    }

    public int[] addFaceCircle(float radius, int divisions, Vector3 center, Plane plane, Facing facing) {
        List<Vector3> points = new List<Vector3>();
        points.Add(center);
        for (int d = 0; d < divisions; d++) {
            float a = d * 2 * Mathf.PI / (float) divisions;
            if (plane == Plane.XY) {
                points.Add(new Vector3(
                    Mathf.Cos(a) * radius + center.x,
                    Mathf.Sin(a) * radius + center.y,
                    0            * radius + center.z));
            } else {
                points.Add(new Vector3(
                    0            * radius + center.x,
                    Mathf.Sin(a) * radius + center.y,
                    Mathf.Cos(a) * radius + center.z));
            }
        }

        List<int> indicies = addVerticies(points);


        for (int d = 1; d < divisions; d++) {
            if (facing == Facing.XPlus) {
                addTriangle(indicies[d], indicies[0], indicies[d + 1]);
            } else {
                addTriangle(indicies[d+1], indicies[0], indicies[d]);
            }
        }
        if (facing == Facing.XPlus) {
            addTriangle(indicies[divisions], indicies[0], indicies[1]);
        } else {
            addTriangle(indicies[1], indicies[0], indicies[divisions]);
        }

        return indicies.ToArray();
    }

    public void addFace4(Vector3[] faceVertices) {
        addTriangle(
            addVertex(faceVertices[0]),
            addVertex(faceVertices[1]),
            addVertex(faceVertices[2])
        );
        addTriangle(
            addVertex(faceVertices[2]),
            addVertex(faceVertices[3]),
            addVertex(faceVertices[0])
        );
    }

    public void addFace3(Vector3[] faceVertices) {
        addTriangle(
            addVertex(faceVertices[0]),
            addVertex(faceVertices[1]),
            addVertex(faceVertices[2])
        );
    }

    public void addFace4i(int[] faceIndicies) {
        addTriangle(
            faceIndicies[0],
            faceIndicies[1],
            faceIndicies[2]
        );
        addTriangle(
            faceIndicies[2],
            faceIndicies[3],
            faceIndicies[0]
        );
    }

    public List<int> addVerticies(List<Vector3> v) {
        List<int> rv = new List<int>();
        foreach (Vector3 point in v)
        {
            int index = addVertex(point);
            rv.Add(index);
        }

        return rv;
    }

    public int addVertex(Vector3 vertex)
    {
        int index = findVertex(vertex);
        if (index == -1) {
            verticies.Add(vertex);
            index = verticies.Count-1;
        }
        return index;
    }

    public void addTriangle(int a, int b, int c)
    {
        triangles.Add(a);
        triangles.Add(b);
        triangles.Add(c);
    }

    // TODO: add some slack
    public int findVertex(Vector3 vertex) {
        for (int i = 0; i < verticies.Count; i++) {
            if (verticies[i].x == vertex.x
                    && verticies[i].y == vertex.y
                    && verticies[i].z == vertex.z) {
                return i;
            }
        }

        return -1;
    }

    Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3[verticies.Count];

        int triangleCount = triangles.Count / 3;
        for (int i = 0; i < triangleCount; i++)
        {
            int normalTriangleIndex = i * 3;
            int vertexIndexA = triangles[normalTriangleIndex];
            int vertexIndexB = triangles[normalTriangleIndex+1];
            int vertexIndexC = triangles[normalTriangleIndex+2];

            Vector3 pointA = verticies[vertexIndexA];
            Vector3 pointB = verticies[vertexIndexB];
            Vector3 pointC = verticies[vertexIndexC];

            Vector3 sideAB = pointB - pointA;
            Vector3 sideAC = pointC - pointA;

            Vector3 triangleNormal = Vector3.Cross(sideAB, sideAC).normalized;

            vertexNormals[vertexIndexA] += triangleNormal;
            vertexNormals[vertexIndexB] += triangleNormal;
            vertexNormals[vertexIndexC] += triangleNormal;
        }

        foreach (Vector3 normal in vertexNormals)
        {
            normal.Normalize();
        }

        return vertexNormals;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh
        {
            vertices = verticies.ToArray(),
            triangles = triangles.ToArray()
        };
        mesh.normals = CalculateNormals();

        Debug.Log("Mesh: verticies: " + verticies.Count + "  triangles: " + triangles.Count / 3);

        return mesh;
    }
}