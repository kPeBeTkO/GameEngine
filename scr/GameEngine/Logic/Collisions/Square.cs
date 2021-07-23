using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Logic.Collisions
{
    public class Square : Body
    {
        public float Size;
        public Square(float size)
        {
            Size = size;
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
                new Vector(Location.X - Size / 2, Location.Y + Size / 2),
                new Vector(Location.X + Size / 2, Location.Y + Size / 2),
                new Vector(Location.X + Size / 2, Location.Y - Size / 2),
                new Vector(Location.X - Size / 2, Location.Y - Size / 2)
            };
        }

        public override bool IsInside(Vector point)
        {
            return (Location.X - Size / 2 < point.X && point.X < Location.X + Size / 2) &&
                   (Location.Y - Size / 2 < point.Y && point.Y < Location.Y + Size / 2);
        }
    }
}
