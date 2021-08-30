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
    class Bullet : Entity
    {
        public Bullet(Vector vector)
        {
            DrawPriority = 2;
            Body = new Box(2, 1);
            Body.Location = vector;
            Collidable = true;
            var im = new Bitmap(1, 2);
            var g = Graphics.FromImage(im);
            g.FillRectangle(Brushes.White, 0, 0, 1, 2);
            var texture = new Texture(im, 1, 2);
            var dic = new Dictionary<string, Texture[]>();
            dic["1"] = new Texture[]{texture};
            State = "1";
            textureHolder = new TextureHolder(dic);
        }

        

        public override void Collide(GameObject obj)
        {
            if (obj is Bullet)
            {
                Dead = true;
            }
            else if (obj is Enemy)
            {
                Dead = true;
            }
            else if (obj is Spaceship)
            {
                Dead = true;
            }
            if (obj is Wall)
            {
               Dead = true;
            }
        }
    }
}
