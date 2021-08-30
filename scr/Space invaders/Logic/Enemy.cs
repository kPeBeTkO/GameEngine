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
    class Enemy : Entity
    {
        private Random random = new Random();
        private int lastShot;
        private Game game;
        static public Bitmap[] textures1 = new Bitmap[] { new Bitmap("Resources\\Enemy1.png"), new Bitmap("Resources\\Enemy2.png"),
            new Bitmap("Resources\\Enemy3.png"), new Bitmap("Resources\\Enemy4.png"), new Bitmap("Resources\\Enemy5.png"), new Bitmap("Resources\\Enemy6.png"),
            new Bitmap("Resources\\Enemy7.png"), new Bitmap("Resources\\Enemy8.png"), new Bitmap("Resources\\Enemy9.png")};
        static public Bitmap[] textures2 = new Bitmap[] { new Bitmap("Resources\\Enemy1.1.png"), new Bitmap("Resources\\Enemy2.1.png"),
            new Bitmap("Resources\\Enemy3.1.png"), new Bitmap("Resources\\Enemy4.1.png"), new Bitmap("Resources\\Enemy5.1.png"),
            new Bitmap("Resources\\Enemy6.1.png"), new Bitmap("Resources\\Enemy7.1.png"), new Bitmap("Resources\\Enemy8.1.png"),
        new Bitmap("Resources\\Enemy9.1.png")};
        public Enemy(Game game, Vector speed)
        {
            this.game = game;
            lastShot = random.Next(0, 20);
            Speed = speed;
            Body = new Box(10, 9);
            Body.Location = new Vector(random.Next(6, 94), 109);
            Collidable = true;
            var type = random.Next(0, 9);
            var texture1 = new Texture(textures1[type], 11, 9, 2);
            var dic = new Dictionary<string, Texture[]>();
            var texture2 = new Texture(textures2[type], 11, 9, 2);
            dic["1"] = new Texture[] { texture1, texture2 };
            State = "1";
            textureHolder = new TextureHolder(dic);
        }

        public void TryShot()
        {
            lastShot++;
            if (lastShot > 40 && random.Next(0, 20) == 1)
            {
                lastShot = 0;
                var bullet = new Bullet(new Vector(Body.Location.X, Body.Location.Y - 8));
                bullet.Speed = new Vector(0, -2);
                Core.AddObject(bullet);
            }
        }

        public override void Collide(GameObject obj)
        {
            if (obj is Bullet)
            {
                game.Score += random.Next(1,15);
                Dead = true;
            }
            if (obj is Deadline)
            {
                Dead = true;
            }
        }
    }
}
