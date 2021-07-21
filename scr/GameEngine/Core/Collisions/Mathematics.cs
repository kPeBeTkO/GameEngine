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
        public static double GetDistanceToSegment(Vector begin, Vector end, Vector point)
        {
            double lenSegAB = begin.DistanceTo(end);
            double lenSegAC = begin.DistanceTo(point);
            double lenSegCB = end.DistanceTo(point);
            double perimetr = (lenSegAB + lenSegAC + lenSegCB) / 2;
            if ((lenSegAB * lenSegAB + lenSegAC * lenSegAC) <= (lenSegCB * lenSegCB) ||
                (lenSegAB * lenSegAB + lenSegCB * lenSegCB) <= (lenSegAC * lenSegAC))
                return Math.Min(lenSegAC, lenSegCB);
            else
                return 2 / lenSegAB * Math.Sqrt(perimetr * (perimetr - lenSegAB) *
                                   (perimetr - lenSegCB) * (perimetr - lenSegAC));
        }
        public static bool SectorIntersection(Vector begin1, Vector end1, Vector begin2, Vector end2)
        {
            if (begin1 == begin2 || begin1 == end2 || end1 == begin2 || end1 == end2) return true;
            if (begin1.X >= end1.X)
            {
                var temp = new Vector(end1.X, end1.Y);
                end1 = new Vector(begin1.X, begin1.Y);
                begin1 = new Vector(temp.X, temp.Y);
            }
            if (begin2.X >= end2.X)
            {
                var temp = new Vector(end2.X, end2.Y);
                end2 = new Vector(begin2.X, begin2.Y);
                begin2 = new Vector(temp.X, temp.Y);
            }
            var k1 = (end1.Y - begin1.Y) / (end1.X - begin1.X);
            var k2 = (end2.Y - begin2.Y) / (end2.X - begin2.X);
            if (k1 == k2) return false;
            double x, y;
            if (k2 == double.NegativeInfinity || k2 == double.PositiveInfinity)
            {
                var b1 = begin1.Y - k1 * begin1.X;
                x = begin2.X;
                y = k1 * x + b1;
            }
            else if (k1 == double.NegativeInfinity || k1 == double.PositiveInfinity)
            {
                var b2 = begin2.Y - k2 * begin2.X;
                x = begin1.X;
                y = k2 * x + b2;
            }
            else
            {
                var b1 = begin1.Y - k1 * begin1.X;
                var b2 = begin2.Y - k2 * begin2.X;
                x = (b2 - b1) / (k1 - k2);
                y = k1 * x + b1;
            }
            return ((begin1.X <= x && x <= end1.X) || (begin1.X >= x && x >= end1.X)) && ((begin2.X <= x && x <= end2.X) || (begin2.X >= x && x >= end2.X))
                && ((begin1.Y <= y && y <= end1.Y) || (begin1.Y >= y && y >= end1.Y)) && ((begin2.Y <= y && y <= end2.Y) || (begin2.Y >= y && y >= end2.Y));
        }
    }
}
