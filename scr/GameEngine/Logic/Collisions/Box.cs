using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Logic.Collisions
{
    public class Box : Body
    {
        public double Width;
        public double Height;
        public Box(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override Vector ClosestPointFrom(Vector point)
        {
            var vertices = GetVertices();
            var closestPoint = Mathematics.GetClosestPoint(vertices[3], vertices[0], point);
            var minLen = closestPoint.DistanceTo(point);
            Vector newPoint;
            double newLen;
            for (int i = 0; i < 3; i++)
            {
                newPoint = Mathematics.GetClosestPoint(vertices[i], vertices[i + 1], point);
                newLen = newPoint.DistanceTo(point);
                if (minLen > newLen)
                {
                    minLen = newLen;
                    closestPoint = newPoint;
                }
            }
            return closestPoint;
        }

        /*public override Vector FirstIntersectionWithRay(Ray ray)
        {
            throw new NotImplementedException();
        }*/
        public bool TryCollision(Box box)
        {
            var vertices = box.GetVertices();
            var thisVertices = GetVertices();
            for (int i = 0; i < 4; i++)
            {
                if (IsInside(vertices[i])) return true;
                if (box.IsInside(thisVertices[i])) return true;
                if (Mathematics.SectorIntersection(vertices[i], vertices[(i + 1) % 4], thisVertices[i], thisVertices[(i + 1) % 4])) return true;
            }
            return false;
        }

        public bool TryCollision(Circle circle)
        {
            return circle.TryCollision(this);
        }

        public override Vector[] GetVertices()
        {
            return new Vector[]
            { 
                new Vector(Location.X - Width / 2, Location.Y + Height / 2),
                new Vector(Location.X + Width / 2, Location.Y + Height / 2),
                new Vector(Location.X + Width / 2, Location.Y - Height / 2),
                new Vector(Location.X - Width / 2, Location.Y - Height / 2)
            };
        }

        public override bool IsInside(Vector point)
        {
            return (Location.X - Width / 2 <= point.X && point.X < Location.X + Width / 2) &&
                   (Location.Y - Height / 2 <= point.Y && point.Y < Location.Y + Height / 2);
        }
    }
}
