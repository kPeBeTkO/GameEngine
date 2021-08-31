using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Logic;
using GameEngine.View.Render.Texture;
using System.Drawing;
using GameEngine.Logic.Collisions;

namespace TestGame
{
    class SolidCircle : Entity, IUpdatable
    {
        Texture texture;
        public SolidCircle(double size, Vector location)
        {
            Body = new Circle(size / 2);
            Body.Location = location;
            var im = new Bitmap(100, 100);
            Solid = true;
            var g = Graphics.FromImage(im);
            g.FillEllipse(Brushes.Black, new RectangleF(0, 0, 100, 100));
            g.Dispose();
            texture = new Texture(im, size, size);
        }


        public override Texture GetTexture()
        {
            return texture;
        }

        public void Update()
        {
            if (Core.MouseLocation != null)
                Body.Location = Core.MouseLocation;
        }
    }
}
