using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedRoadMesh : BaseMakeMesh
{
    public double n = 10;   // 2n triangles will be created
    public double t = 2;    // PI / t is central angle of road

    protected override void SetVertices()
    {
        double pi = Math.PI;

        for (int k = 0; k <= n; k++)
        {
            double theta = (k / n) * pi / t;
            float sinTheta = (float)Math.Sin(theta);
            float cosTheta = (float)Math.Cos(theta);

            vertices.Add(new Vector3(hsize * sinTheta, 0, hsize * cosTheta));
            vertices.Add(new Vector3(size * sinTheta, 0, size * cosTheta));
        }
    }

    protected override void SetNormals()
    {
        for (int i = 0; i < 2 * (n + 1); i++)
        {
            normals.Add(new Vector3(0f, -1f, 0f));
        }
    }

    protected override void SetUV()
    {
        for (int k = 0; k <= n; k++)
        {
            uv.Add(new Vector2((float)(k / n), 0));
            uv.Add(new Vector2((float)(k / n), 1));
        }
    }

    protected override void SetTriangles()
    {
        for (int i = 0; i < n; i++)
        {
            int start = 2 * i;

            triangles.Add(start);
            triangles.Add(start + 1);
            triangles.Add(start + 2);

            triangles.Add(start + 2);
            triangles.Add(start + 1);
            triangles.Add(start + 3);
        }
    }
}
