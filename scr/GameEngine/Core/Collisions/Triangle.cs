using GameEngine.Core.Collisions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core
{
    class Triangle
    {
        public Vector A;
        public Vector B;
        public Vector C;
        public Triangle(Vector point1, Vector point2, Vector point3)
        {
            this.A = point1;
            this.B = point2;
            this.C = point3;
        }

        public Triangle(Triangle t)
        {
            this.A = t.A;
            this.B = t.B;
            this.C = t.C;
        }

        public static Triangle operator +(Triangle t1, Triangle t2)
        {
            return new Triangle(t1.A + t2.A, t1.B + t2.B, t1.C + t2.C);
        }

        public bool IsPointInTriangle(Vector point)
        {
            var a = Mathematics.VecMultiply(A, B, point);
            var b = Mathematics.VecMultiply(B, C, point);
            var c = Mathematics.VecMultiply(C, A, point);
            // Если все три тройки векторов однонаправленные, то  точка(x,y) внутри треугольника
            return (a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0) ? true : false;
        }
    }
}
