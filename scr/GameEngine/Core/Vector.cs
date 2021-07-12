using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core
{
    public class Vector
    {
        public readonly float X;
        public readonly float Y;
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }
    }
}
