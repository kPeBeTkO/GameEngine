using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Collisions
{
    public class Circle : Body
    {
        public double Radius;
        public Circle(double radius)
        {
            Radius = radius;
        }

        public override Vector ClosestPointFrom(Vector point)
        {
            var vec = point - Location;
            return vec / (vec.Length * Radius) + Location;
        }

        public bool TryCollision(Box box)
        {
            var vertices = box.GetVertices();
            for (int i = 0; i < 4; i++)
                if (Mathematics.GetDistanceToSegment(vertices[i], vertices[(i + 1)% 4], Location) <= Radius) return true;
            return false;
        }
        public bool TryCollision(Circle circle)
        {
            return Radius + circle.Radius >= Location.DistanceTo(circle.Location);
        }

        public override Vector[] GetVertices()
        {
            throw new NotImplementedException();
        }

        public override bool IsInside(Vector point)
        {
            return (point.X - Location.X) * (point.X - Location.X) + (point.Y - Location.Y) * (point.Y - Location.Y) <= Radius * Radius;
        }
    }
}
