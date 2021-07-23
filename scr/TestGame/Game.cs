using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine.Logic;
using GameEngine.Logic.Physics;
using GameEngine.Logic.Collisions;
using GameEngine.Render;
using System.Drawing;

namespace TestGame
{
    class Game : Form
    {
        public HashSet<Keys> keysPresed = new HashSet<Keys>();
        public Game()
        {
            DoubleBuffered = true;
            //var player = new SolidBox(1, 1, new Vector(1, 5));
            var player = new SolidCircle(1, new Vector(1, 5));
            var wall = new SolidCircle(2, new Vector(6, 5)) {Fixed = true };
            var wall2 = new SolidBox(2, new Vector(6, 7.5)){Fixed = true };
            Core.Physics = new SimplePhysics();
            Core.Objects = new List<GameObject>(){ player, wall, wall2};
            var cam = new Camera();
            cam.Frame = new Box(10, 10);
            cam.Frame.Location = new Vector(5, 5);
            Width = 600;
            Height = 600;
            Paint += (s, a) =>
            {
                var frame = cam.DrawFrame(new Size(600, 600));
                a.Graphics.DrawImage(frame, 0, 0);
            };
            Invalidate();
            var timer = new Timer();
            timer.Interval = 50;
            timer.Tick += (s, a) => 
            {
                player.Speed = GetSpeed();
                Core.Update();
                Invalidate();
            };
            timer.Start();
            
            KeyDown += (s, a) => keysPresed.Add(a.KeyCode);
            KeyUp += (s, a) => keysPresed.Remove(a.KeyCode);
        }

        public Vector GetSpeed()
        {
            var Dir = new Vector(0, 0);
            foreach(var key in keysPresed)
                switch(key)
                {
                    case Keys.W:
                        Dir += new Vector(0, 1);
                        break;
                    case Keys.A:
                        Dir += new Vector(-1, 0);
                        break;
                    case Keys.S:
                        Dir += new Vector(0, -1);
                        break;
                    case Keys.D:
                        Dir += new Vector(1, 0);
                        break;
                }
            return Dir * 0.1;
        }

    }
}
