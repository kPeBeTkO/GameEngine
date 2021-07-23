using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Logic.Collisions
{
    public abstract class Body
    {
        public Vector Location;

        public virtual Vector FirstIntersectionWithRay(Ray ray)
        {
            var vert = GetVertices();
            if (vert == null || vert.Length < 2)
                throw new Exception();
            var a = vert[0];
            Vector min = null;
            var minDist = double.PositiveInfinity;
            for (var i = 1; i < vert.Length; i++)
            {
                var b = vert[i];
                var point = ray.IntersectionWithSegment(a, b);
                if (point != null) 
                { 
                    var dist = ray.Location.DistanceTo(point);
                    if(min == null ||  dist < minDist )
                    {
                        min = point;
                        minDist = dist;
                    }
                }
            }
            return min;
        }
        public static bool CheckCollision(Body body1, Body body2)
        {
            if (body1 is Circle circle1 && body2 is Circle circle2)
            {
                return circle1.TryCollision(circle2);
            }
            if (body1 is Box box1 && body2 is Box box2)
            {
                return box1.TryCollision(box2);
            }
            if (body1 is Circle circle && body2 is Box box)
            {
                return circle.TryCollision(box);
            }
            if (body2 is Circle circle3 && body1 is Box box3)
            {
                return circle3.TryCollision(box3);
            }
            throw new NotImplementedException();
        }

        public abstract bool IsInside(Vector point);
        public abstract Vector ClosestPointFrom(Vector point);
        public abstract Vector[] GetVertices();

        public override string ToString()
        {
            return Location.ToString();
        }
    }
}
