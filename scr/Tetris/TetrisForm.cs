using GameBase;
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
    class TetrisForm : BaseForm
    {
        public TetrisForm() : base(20)
        {
            
            Width = 600;
            Height = 800;
            var width = 10;
            var height = 15;
            Cam.Frame = new Box(width + 2, height + 1);
            Cam.Frame.Location = new Vector(width / 2 + 0.5, height / 2 - 0.5);
            var game = new Game(width, height);
            Core.AddScript(game);
            
            Timer.Start();
        }
    }
}
