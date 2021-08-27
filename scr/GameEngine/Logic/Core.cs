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
        public static IPhysics Physics;

        public static IReadOnlyList<GameObject> Objects => objects;
        private static List<GameObject> objects = new List<GameObject>();
        private static List<GameObject> newObjects = new List<GameObject>();

        public static IReadOnlyList<IUpdatable> Scripts => scripts;
        private static List<IUpdatable> scripts = new List<IUpdatable>();
        private static List<IUpdatable> newScripts = new List<IUpdatable>();
        private static List<IUpdatable> endedScripts = new List<IUpdatable>();
        
        public static IReadOnlyList<KeyInput> KeysPressed => keysPressed;
        private static List<KeyInput> keysPressed = new List<KeyInput>();
        
        public static IReadOnlyCollection<string> ActiveKeys => activeKeys;
        private static HashSet<string> activeKeys = new HashSet<string>();

        public static Vector MouseLocation;

        public static void Update()
        {
            objects.AddRange(newObjects);
            newObjects.Clear();
            scripts.AddRange(newScripts);
            newScripts.Clear();

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
                    if (Body.CheckCollision(collidable[i].Body, collidable[j].Body))
                    {
                        collidable[i].Collide(collidable[j]);
                        collidable[j].Collide(collidable[i]);
                    }

            keysPressed.Clear();
            objects.RemoveAll(o => o.Dead);
            foreach(var script in endedScripts)
            {
                scripts.Remove(script);
            }
            endedScripts.Clear();
        }

        public static void Input(KeyInput input)
        {
            keysPressed.Add(input);
            if (input.Release)
                activeKeys.Remove(input.Key);
            else
                activeKeys.Add(input.Key);
        }

        public static void AddObject(GameObject obj)
        {
            newObjects.Add(obj);
        }

        public static void AddObjects(IEnumerable<GameObject> objects)
        {
            newObjects.AddRange(objects);
        }

        public static void AddScript(IUpdatable script)
        {
            newScripts.Add(script);
        }


        public static void Clear()
        {
            objects.Clear();
            scripts.Clear();
            activeKeys.Clear();
            keysPressed.Clear();
        }
    }
}
