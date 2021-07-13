using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Collisions
{
    public class Ray
    {
        public Vector Location;
        public Vector Direction;
        public Ray(Vector location, Vector direction)
        {
            Location = location;
            Direction = direction;
        }

        public Vector IntersectionWithSegment(Vector a, Vector b)
        {
            var x1 = Location.X; var y1 = Location.Y;
            var x2 = (Location + Direction).X; var y2 = (Location + Direction).Y;
            var x3 = a.X; var y3 = a.Y;
            var x4 = b.X; var y4 = b.Y;
            var den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (den == 0)
                return null;
            var t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            var u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;
            if (u >= 0 && u <= 1 && t >= 0)
                return new Vector(x1 + t * (x2 - x1), y1 + t * (y2 - y1));
            return null;
        }
    }
}
