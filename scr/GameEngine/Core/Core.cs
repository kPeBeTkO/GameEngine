using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameEngine.Core
{
    public static class Core
    {
        public static double PointsPerPixel;
        public static List<GameObject> Walls;
        public static List<Entity> Entities;

        public static void Update()
        {
            foreach(var wall in Entities)
                wall.Move();
        }
    }
}
