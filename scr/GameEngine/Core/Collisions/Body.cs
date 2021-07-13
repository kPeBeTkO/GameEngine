using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Collisions
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

        public abstract bool IsInside(Vector point);
        public abstract Vector ClosestPointFrom(Vector point);
        public abstract Vector[] GetVertices();
    }
}
