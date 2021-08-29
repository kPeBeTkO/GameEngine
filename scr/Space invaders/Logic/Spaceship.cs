using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Logic;
using GameEngine.Logic.Collisions;
using GameEngine.View.Render.Texture;

namespace SpaceInvaders.Logic
{
    class Spaceship : Entity, IUpdatable
    {
        Game game;
        int lastShot;
        public Spaceship(Game game, int width, int height)
        {
            this.game = game;
            Body = new Box(11, 8);
            Body.Location = new Vector(width, height);
            Collidable = true;
            Solid = true;
            var im = new Bitmap("Resources\\Spaceship.png");
            var texture = new Texture(im, new SizeF(11, -8));
            Solid = true;
            var dic = new Dictionary<string, Texture[]>();
            dic["1"] = new Texture[] { texture };
            State = "1";
            textureHolder = new TextureHolder(dic);
        }

        public void Shot()
        {
            if (lastShot > 10)
            {
                var bullet = new Bullet(new Vector(Body.Location.X, Body.Location.Y + 5));
                bullet.Speed = new Vector(0, 2);
                Core.AddObject(bullet);
                lastShot = 0;
            }
        }

        public void Update()
        {
            lastShot++;
            Speed = new Vector(0, 0);
            if (Core.ActiveKeys.Contains("Left"))
                Speed += new Vector(-3, 0);
            if (Core.ActiveKeys.Contains("Right"))
                Speed += new Vector(3, 0);
            foreach (var key in Core.KeysPressed)
            {
                if (key.Key == "Up" && key.Press)
                {
                    Shot();
                    break;
                }
            }
        }

        public override void Collide(GameObject obj)
        {
            if (obj is Bullet)
            {
                game.Lives--;
            }
        }
    }
}
