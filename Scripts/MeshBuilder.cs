// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder
{
    List<Vector3> verticies = new List<Vector3>();
    List<int> triangles = new List<int>();

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

    public int addVertex(Vector3 vertex)
    {
        // TODO: reuse the existing vertex in verticies if found
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