using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Collisions
{
    static class Mathematics
    {
        public static double VecMultiply(Vector A, Vector B, Vector C)
        {
            return ((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X));
        }
        public static Triangle[] Triangulation(Polygon body)
        {
            var triangles = new Triangle[body.GetVertices().Length - 2];
            var deletedPoints = new bool[body.GetVertices().Length];
            int i = 0;
            int point = 0;
            var f = false;
            while (i < triangles.Length)
            {
                var p1 = point;
                while (deletedPoints[p1])
                    p1 = (p1 + 1) % body.GetVertices().Length;
                var p2 = (p1 + 1) % body.GetVertices().Length;
                while (deletedPoints[p2])
                    p2 = (p2 + 1) % body.GetVertices().Length;
                var p3 = (p2 + 1) % body.GetVertices().Length;
                while (deletedPoints[p3])
                    p3 = (p3 + 1) % body.GetVertices().Length;
                if (VecMultiply(body.GetVertices()[p1], body.GetVertices()[p2], body.GetVertices()[p3]) >= 0)
                {
                    var triangle = new Triangle(body.GetVertices()[p1], body.GetVertices()[p2], body.GetVertices()[p3]);
                    for (int j = 0; j < body.GetVertices().Length; j++)
                        if (j != p1 && j != p2 && j != p3 && !deletedPoints[j] && triangle.IsPointInTriangle(body.GetVertices()[j]))
                        {
                            f = true;
                            break;
                        }
                    if (!f)
                    {
                        triangles[i] = new Triangle(body.GetVertices()[p1], body.GetVertices()[p2], body.GetVertices()[p3]);
                        deletedPoints[p2] = true;
                        i++;
                    }
                    else
                        f = false;
                }
                point = (p1 + 1) % body.GetVertices().Length;
            }
            return triangles;
        }
    }
}
