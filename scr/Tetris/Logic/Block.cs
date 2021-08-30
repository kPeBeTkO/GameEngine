using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Logic;
using GameEngine.Logic.Collisions;
using System.Drawing;
using GameEngine.View.Render.Texture;

namespace Tetris.Logic
{
    class Block : GameObject
    {
        public Color Color;
        public Figure Figure;
        Texture texture; 
        public Block(int x, int y, Color color, Figure figure)
        {
            Body = new Box(1, 1);
            Body.Location = new Vector(x, y);
            Color = color;
            Figure = figure;
            Collidable = true;
            var im = new Bitmap(100, 100);
            var g = Graphics.FromImage(im);
            g.FillRectangle(new SolidBrush(color), new RectangleF(0, 0, 100, 100));
            g.DrawRectangle(new Pen(Brushes.Black, 10), new Rectangle(0, 0, 100, 100));
            g.Dispose();
            texture = new Texture(im, 1, 1);
        }

        public override Texture GetTexture()
        {
            return texture;
        }
    }
}
