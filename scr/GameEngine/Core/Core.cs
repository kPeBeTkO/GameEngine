using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameEngine.Core.Physics;

namespace GameEngine.Core
{
    public static class Core
    {
        public static List<GameObject> Objects;
        public static IPhysics Physics;

        public static void Update()
        {
            foreach(var obj in Objects)
                Physics.MoveObject(obj);
        }
    }
}
