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
    class CollisionChecker : GameObject, IUpdatable
    {
        Texture red;
        Texture blue;
        Texture texture;
        public CollisionChecker(double size, Vector location) : this(size, size, location)
        {
            
        }

        public CollisionChecker(double width, double height, Vector location)
        {
            Body = new Circle(width / 2);
            Body = new Box(width, height);
            Body.Location = location;
            Collidable = true;
            Solid = true;
            var im = new Bitmap(100, 100);
            var g = Graphics.FromImage(im);
            g.FillEllipse(Brushes.Red, new RectangleF(0, 0, 100, 100));
            g.FillRectangle(Brushes.Red, new RectangleF(0, 0, 100, 100));
            g.Dispose();
            red = new Texture(im, width, height);
            im = new Bitmap(100, 100);
            g = Graphics.FromImage(im);
            g.FillEllipse(Brushes.Blue, new RectangleF(0, 0, 100, 100));
            g.FillRectangle(Brushes.Blue, new RectangleF(0, 0, 100, 100));
            g.Dispose();
            blue = new Texture(im, width, height);
        }


        public override Texture GetTexture()
        {
            return texture;
        }

        public override void Collide(GameObject obj)
        {
            texture = red;
        }

        public void Update()
        {
            texture = blue;
        }
    }
}
