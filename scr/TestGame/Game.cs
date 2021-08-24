using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine.Logic;
using GameEngine.Logic.Physics;
using GameEngine.Logic.Collisions;
using GameEngine.View.Render;
using GameBase;
using System.Drawing;

namespace TestGame
{
    class Game : BaseForm
    {
        public HashSet<Keys> keysPresed = new HashSet<Keys>();
        public Game() : base(20)
        {
            DoubleBuffered = true;
            var player = new SolidBox(1, 1, new Vector(1, 5));
            Core.Objects = new List<GameObject>(){ player};
            FrameLocation = new Rectangle(0, 0, Width, Height);
            Cam.Frame = new Box(10, 10);
            Cam.Frame.Location = new Vector(5, 5);
            Width = 600;
            Height = 600;
            
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
