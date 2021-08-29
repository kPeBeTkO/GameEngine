using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameBase;
using GameEngine.Logic;
using GameEngine.Logic.Collisions;
using GameEngine.Logic.Physics;
using GameEngine.View.Render;
using SpaceInvaders.Logic;

namespace SpaceInvaders
{
    public partial class SpaceForm : BaseForm
    {
        Game game;
        public SpaceForm() : base(20)
        {
            Width = 800;
            Height = 800;
            var width = 100;
            var height = 100;
            Cam.Frame = new Box(width, height);
            Cam.Frame.Location = new Vector(width / 2, height / 2);
            
            game = new Game();
            Core.Physics = new SimplePhysics();
            Core.AddScript(game);
            Timer.Start();
        }

        public override void RenderBack(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Black,0,0,1000, 1000);
        }

        public override void RenderGui(Graphics graphics)
        {
            graphics.DrawString(game.Lives.ToString(), new Font("impact", 40), Brushes.Yellow, new PointF(700, 20));
            graphics.DrawString("score " + game.Score.ToString(), new Font("impact", 40), Brushes.Blue, new PointF(0, 20));
        }
    }
}
