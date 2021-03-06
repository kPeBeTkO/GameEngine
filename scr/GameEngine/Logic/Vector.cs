using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Logic
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

        public bool IsValid()
        {
            return !double.IsNaN(X) && !double.IsNaN(Y) && !double.IsInfinity(X) && !double.IsInfinity(Y);
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
        public static Vector operator /(Vector vector, double divider)
        {
            return new Vector(vector.X / divider, vector.Y / divider);
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }
        public static bool operator ==(Vector v1, Vector v2)
        {
            if (v1 is null || v2 is null)
                return v1 is null && v2 is null;
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector vector)
            {
                return vector == this;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (int)((X.GetHashCode() * 65536L + Y.GetHashCode()) % int.MaxValue);
        }

        public override string ToString()
        {
            return X + " " + Y;
        }
    }
}
