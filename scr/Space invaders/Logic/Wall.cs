using GameEngine.Logic;
using GameEngine.Logic.Collisions;
using GameEngine.View.Render.Texture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Logic
{
    class Wall : GameObject
    {
        public Wall(Vector size, Vector location)
        {
            Body = new Box(size.X, size.Y);
            Body.Location = location;
            Solid = true;
            Collidable = true;
            var im = new Bitmap((int)size.X, (int)size.Y);
            var g = Graphics.FromImage(im);
            g.FillRectangle(Brushes.Yellow, 0, 0, (float)size.X, (float)size.Y);
            var texture = new Texture(im, new SizeF((float)size.X, (float)size.Y));
            var dic = new Dictionary<string, Texture[]>();
            dic["1"] = new Texture[] { texture };
            State = "1";
            textureHolder = new TextureHolder(dic);
        }
    }
}
