using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameEngine.Logic.Collisions;
using GameEngine.Logic.Physics;
using System.Linq;
using System.Windows.Forms;

namespace GameEngine.Logic
{
    public static class Core
    {
        public static List<GameObject> Objects = new List<GameObject>();
        public static IPhysics Physics;
        public static List<IUpdatable> Scripts = new List<IUpdatable>();

        private static List<Keys> keysPressed = new List<Keys>();
        public static IReadOnlyList<Keys> KeysPressed => keysPressed;

        private static HashSet<Keys> activeKeys = new HashSet<Keys>();
        public static IReadOnlyCollection<Keys> ActiveKeys => activeKeys;

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

        public static void KeyDown(Keys key)
        {
            keysPressed.Add(key);
            activeKeys.Add(key);
        }

        public static void KeyUp(Keys key)
        {

            activeKeys.Remove(key);
        }
    }
}
