using GameEngine.Logic;
using GameEngine.Logic.Collisions;
using GameEngine.View.Render.Texture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class SolidBox : Entity, IUpdatable
    {
        Texture texture;
        public SolidBox(double size, Vector location) : this(size, size, location)
        {
            
        }

        public SolidBox(double width, double height, Vector location)
        {
            DrawPriority = 1;
            Collidable = true;
            Body = new Box(width, height);
            Speed = new Vector(0, 0);
            Body.Location = location;
            var im = new Bitmap(100, 100);
            Solid = true;
            var g = Graphics.FromImage(im);
            g.FillRectangle(Brushes.Black, new RectangleF(0, 0, 100, 100));
            g.Dispose();
            texture = new Texture(im, width, height);
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
