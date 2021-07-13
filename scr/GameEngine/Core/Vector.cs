using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core
{
    public class Vector
    {
        public readonly double X;
        public readonly double Y;
        public double Angle => Math.Atan2(Y, X);
        public double Length => Math.Sqrt(X*X + Y*Y);
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Vector other)
        {
            return (this - other).Length;
        }

        public static Vector FromAngle(double angle, double length)
        {
            return new Vector(Math.Cos(angle) * length, Math.Sin(angle) * length);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator *(Vector vector, double multiplaer)
        {
            return new Vector(vector.X * multiplaer, vector.Y * multiplaer);
        }
    }
}
