using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Habrador_Computational_Geometry;

public class MeshHandler : MonoBehaviour
{
    List<MyVector2> points = new List<MyVector2>();
    Vector3[] vertices;
    Mesh mesh;
    List<Vector3> vectors = new List<Vector3>();
    HashSet<Triangle2> triangles;
    int[] arrTriangles;
    public MeshFilter meshFilter;

    public void CreateShape(List<Vector3> vectorList)
    {
        vectors = vectorList; 
        setUp();
        buildTriangles();

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = arrTriangles;
        mesh.RecalculateNormals();
    }

    public void setUp()
    {
        MyVector2 temp = new MyVector2();
        vertices = new Vector3[vectors.Count];
        int setupIndex = 0;

        // The shape generation package requires its own objects for vectors
        foreach (Vector3 vector in vectors)
        {
            temp.x = vector.x;
            temp.y = vector.z;
            vertices[setupIndex].x = vector.x;
            vertices[setupIndex].z = vector.z;
            points.Add(temp);
            setupIndex++;
        }
        // Call shape generation tool
        triangles = _EarClipping.Triangulate(points);

        // Set up mesh
        mesh = new Mesh();
        GameObject meshGenerator = GameObject.Find("/Mesh");
        meshFilter = meshGenerator.GetComponent(typeof(MeshFilter)) as MeshFilter;
        meshGenerator.GetComponent<MeshFilter>().mesh = mesh;
    }

    // The triangles need to be arranged from the output from shape generation tool before the mesh can handle them
    public void buildTriangles()
    {
        arrTriangles = new int[triangles.Count * 3];
        int index = 0;
        int[] temp = new int[3];
        // For each triangle three points need to be found and numbered for the mesh to use - the order is very important
        foreach (Triangle2 triangle in triangles)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].x == triangle.p1.x && vertices[i].z == triangle.p1.y)
                {
                    temp[0] = i;
                }
                else if (vertices[i].x == triangle.p2.x && vertices[i].z == triangle.p2.y)
                {
                    temp[1] = i;
                }
                else if (vertices[i].x == triangle.p3.x && vertices[i].z == triangle.p3.y)
                {
                    temp[2] = i;
                }
            }
            int tempIndex = 0;
            for (int j = index; j < index + 3; j++)
            {
                arrTriangles[j] = temp[tempIndex];
                tempIndex++;
            }
            index = index + 3;
        }
    }
}

