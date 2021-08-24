using GameEngine.Logic;
using GameEngine.Logic.Collisions;
using GameEngine.View.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Logic;

namespace Tetris
{
    class TetrisForm : Form
    {
        public TetrisForm()
        {
            DoubleBuffered = true;
            Width = 600;
            Height = 800;
            var width = 10;
            var height = 15;
            var cam = new Camera();
            cam.Frame = new Box(width + 2, height + 1);
            cam.Frame.Location = new Vector(width / 2 + 0.5, height / 2 - 0.5);
            var game = new Game(width, height);
            Core.Scripts.Add(game);
            Paint += (s, a) =>
            {
                var frame = cam.DrawFrame(new Size(Width, Height));
                a.Graphics.DrawImage(frame, 0, 0);
            };
            Invalidate();
            var timer = new Timer();
            timer.Interval = 25;
            timer.Tick += (s, a) => 
            {
                Core.Update();
                Invalidate();
            };
            timer.Start();
            
            KeyDown += (s, a) => 
            {
                switch(a.KeyCode)
                {
                    case Keys.Up:
                        game.CurrentFigure.Rotate();
                        break;
                    case Keys.Right:
                        game.CurrentFigure.MoveRight();
                        break;
                    case Keys.Left:
                        game.CurrentFigure.MoveLeft();
                        break;
                    case Keys.Down:
                        game.SkipDown();
                        break;
                }
            };
        }
    }
}
