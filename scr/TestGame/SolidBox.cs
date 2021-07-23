using GameEngine.Logic;
using GameEngine.Logic.Collisions;
using GameEngine.Render.Texture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class SolidBox : Entity
    {
        Texture texture;
        public SolidBox(double size, Vector location) : this(size, size, location)
        {
            
        }

        public SolidBox(double width, double height, Vector location)
        {
            Body = new Box(width, height);
            Body.Location = location;
            var im = new Bitmap(100, 100);
            Solid = true;
            var g = Graphics.FromImage(im);
            g.FillRectangle(Brushes.Black, new RectangleF(0, 0, 100, 100));
            g.Dispose();
            texture = new Texture(im, new SizeF((float)width, (float)height));
        }


        public override Texture GetTexture()
        {
            return texture;
        }
    }
}
