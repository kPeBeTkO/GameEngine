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
            var idle = new Texture(im, 11, 8);
            im = new Bitmap("Resources\\SpaceshipAtack.png");
            var shoot = new Texture(im, 11, 11);
            Solid = true;
            var dic = new Dictionary<string, Texture[]>();
            dic["idle"] = new Texture[] { idle };
            dic["shot"] = new Texture[] { shoot };
            State = "idle";
            textureHolder = new TextureHolder(dic);
        }

        public void Shot()
        {
            if (lastShot > 10)
            {
                State = "shot";
                var bullet = new Bullet(new Vector(Body.Location.X, Body.Location.Y + 5));
                //var dir = Core.MouseLocation - Body.Location;
                //bullet.Speed = dir * (2 / dir.Length);
                bullet.Speed = new Vector(0, 2);
                Core.AddObject(bullet);
                lastShot = 0;
            }
        }

        public void Update()
        {
            State = "idle";
            lastShot++;
            Speed = new Vector(0, 0);
            if (Core.ActiveKeys.Contains("Left"))
                Speed += new Vector(-3, 0);
            if (Core.ActiveKeys.Contains("Right"))
                Speed += new Vector(3, 0);
            foreach (var key in Core.KeysPressed)
            {
                if (key.Key == "Space" && key.Press)
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
