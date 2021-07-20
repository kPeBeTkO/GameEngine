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
                if (!IsInside(vertices[i])) return true;
            return false;
        }
        public bool TryCollision(Circle circle)
        {
            return IsInside(ClosestPointFrom(circle.Location)) || circle.IsInside(ClosestPointFrom(Location));
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
