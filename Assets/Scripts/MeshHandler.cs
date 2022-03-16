using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sebastian.Geometry;

public class MeshHandler : MonoBehaviour
    {
        public MeshFilter meshFilter;

        public void CreateShape(List<Vector3> vectorList)
        {
            Shape shape = new Shape();
            List<Shape> shapes = new List<Shape>();
            shapes.Add(shape);

            shape.points = vectorList;

            CompositeShape compShape = new CompositeShape(shapes);
            meshFilter.mesh = compShape.GetMesh();
        }
    }

