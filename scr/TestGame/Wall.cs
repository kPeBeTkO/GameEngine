using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Core;
using GameEngine.Render.Texture;
using System.Drawing;
using GameEngine.Core.Collisions;

namespace TestGame
{
    class Wall : Entity
    {
        Texture texture;
        public Wall()
        {
            Body = new Square(1);
            Body.Location = new Vector(5, 5);
            var im = new Bitmap(100, 100);
            Speed = new Vector(0.1, 0);
            var g = Graphics.FromImage(im);
            g.FillRectangle(Brushes.Black, new RectangleF(0, 0, 100, 100));
            g.Dispose();
            texture = new Texture(im, new SizeF(1, 1));
        }

        public override void Collide(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override Texture GetTexture()
        {
            return texture;
        }
    }
}
