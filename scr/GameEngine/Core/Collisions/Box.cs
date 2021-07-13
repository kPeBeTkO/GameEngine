using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Collisions
{
    public class Box : Body
    {
        public float Width;
        public float Height;
        public Box(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public override Vector ClosestPointFrom(Vector point)
        {
            throw new NotImplementedException();
        }

        /*public override Vector FirstIntersectionWithRay(Ray ray)
        {
            throw new NotImplementedException();
        }*/

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
            return (Location.X - Width / 2 < point.X && point.X < Location.X + Width / 2) &&
                   (Location.Y - Height / 2 < point.Y && point.Y < Location.Y + Height / 2);
        }
    }
}
