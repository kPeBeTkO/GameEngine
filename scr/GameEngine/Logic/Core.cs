using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameEngine.Logic.Collisions;
using GameEngine.Logic.Physics;
using System.Linq;

namespace GameEngine.Logic
{
    public static class Core
    {
        public static List<GameObject> Objects = new List<GameObject>();
        public static IPhysics Physics;
        public static List<IUpdatable> Scripts = new List<IUpdatable>();

        private static List<KeyInput> keysPressed = new List<KeyInput>();
        public static IReadOnlyList<KeyInput> KeysPressed => keysPressed;

        private static HashSet<string> activeKeys = new HashSet<string>();
        public static IReadOnlyCollection<string> ActiveKeys => activeKeys;

        public static Vector MouseLocation;

        public static void Update()
        {
            foreach(var script in Scripts)
                script.Update();
            foreach(var obj in Objects)
            {
                if (obj is IUpdatable ent)
                    ent.Update();
                if (Physics != null)
                    Physics.MoveObject(obj);
            }
            var collidable = Objects.Where(o => o.Collidable).ToArray();
            for (var i = 0; i < collidable.Length; i++)
                for (var j = i + 1; j < collidable.Length; j++)
                    collidable[i].Collide(collidable[j]);
        }

        public static void Input(KeyInput input)
        {
            keysPressed.Add(input);
            if (input.Release)
                activeKeys.Remove(input.Key);
            else
                activeKeys.Add(input.Key);
        }
    }
}
