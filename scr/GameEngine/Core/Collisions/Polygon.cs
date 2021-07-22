using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Collisions
{
    public class Polygon : Body
    {
        public Vector[] Verteces;
        private Triangle[] Triangles;
        private Vector center;
        public Polygon(Vector[] v, Vector loc)
        {
            Verteces = v;
            Location = loc;
            MoveToZero();
            Triangles = Mathematics.Triangulation(this);
        }
        private void MoveToZero()
        {
            var Left = Verteces[0].X;
            var Top = Verteces[0].Y;
            var Right = Verteces[0].X;
            var Down = Verteces[0].Y;
            foreach (var point in Verteces)
            {
                Left = Math.Min(Left, point.X);
                Top = Math.Max(Top, point.Y);
                Right = Math.Max(Right, point.X);
                Down = Math.Min(Down, point.Y);
            }
            center = new Vector((Right - Left) / 2, (Top - Down) / 2);
            var newVerteces = new Vector[Verteces.Length];
            for (int i = 0; i < Verteces.Length; i++)
                newVerteces[i] = new Vector(Verteces[i].X - Left - center.X, Verteces[i].Y - Down - center.Y);
            Verteces = newVerteces;
        }
        static bool TryCollision(Polygon body1, Polygon body2)
        {
            var body = new Polygon[2] { body1, body2 };
            for (int k = 0; k < 2; k++)
            {
                var triangles = body[k].Triangles;
                for (int i = 0; i < triangles.Length; i++)
                {
                    var triangleLocation = new Triangle(triangles[i].A + body[k].Location, triangles[i].B + body[k].Location, triangles[i].C + body[k].Location);
                    for (int j = 0; j < body[k].Verteces.Length; j++)
                        if (triangleLocation.IsPointInTriangle(body[k].Verteces[j] + body[(k + 1) % 2].Location))
                            return true;
                }
            }
            return false;
        }

        public override Vector ClosestPointFrom(Vector point)
        {
            /*if (IsInside(point))
                return point;
            var closest = Verteces[0];
            for (int i = 0; i < Verteces.Length; i++)
            {
                if ()
            }
            public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
            {
                double lenSegAB = GetLength(ax, bx, ay, by);
                double lenSegAC = GetLength(ax, x, ay, y);
                double lenSegCB = GetLength(x, bx, y, by);
                double perimetr = (lenSegAB + lenSegAC + lenSegCB) / 2;
                if ((lenSegAB * lenSegAB + lenSegAC * lenSegAC) <= (lenSegCB * lenSegCB) ||
                    (lenSegAB * lenSegAB + lenSegCB * lenSegCB) <= (lenSegAC * lenSegAC))
                    return Math.Min(lenSegAC, lenSegCB);
                else
                    return 2 / lenSegAB * Math.Sqrt(perimetr * (perimetr - lenSegAB) * 
                                       (perimetr - lenSegCB) * (perimetr - lenSegAC));
            }
             */
            throw new NotImplementedException();
        }

        public override Vector[] GetVertices()
        {
            var newVerteces = new Vector[Verteces.Length];
            for (int i = 0; i < Verteces.Length; i++)
                newVerteces[i] = new Vector(Verteces[i].X + Location.X, Verteces[i].Y + Location.Y);
            return newVerteces;
        }

        public override bool IsInside(Vector point)
        {
            var triangles = Triangles;
            for (int i = 0; i < triangles.Length; i++)
            {
                var triangleLocation = new Triangle(triangles[i].A + Location, triangles[i].B + Location, triangles[i].C + Location);
                for (int j = 0; j < Verteces.Length; j++)
                    if (triangleLocation.IsPointInTriangle(point))
                        return true;
            }
            return false;
        }
    }
}
