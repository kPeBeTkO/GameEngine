using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine.Core;
using GameEngine.Core.Physics;
using GameEngine.Core.Collisions;
using GameEngine.Render;
using System.Drawing;

namespace TestGame
{
    class Game : Form
    {
        public Game()
        {
            DoubleBuffered = true;
            var player = new SolidCircle(1, new Vector(3.5, 5));
            player.Speed = new Vector(0.1, -0.01);
            var wall = new SolidCircle(2, new Vector(6, 5));
            //var wall2 = new SolidCircle(2, new Vector(6, 5));
            wall.Speed = new Vector(0, 0);
            Core.Physics = new SimplePhysics();
            Core.Objects = new List<GameObject>(){ wall, player};
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
                Core.Update();
                Invalidate();
            };
            timer.Start();
        }
    }
}
