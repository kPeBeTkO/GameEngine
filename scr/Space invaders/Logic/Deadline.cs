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
    class Deadline : GameObject
    {
        Game game;
        public Deadline(Game game)
        {
            this.game = game;
            Body = new Box(1000, 1);
            Body.Location = new Vector(25, 20);
            Collidable = true;
            var texture = new Texture(new Bitmap(1,1), 1, 1);
            var dic = new Dictionary<string, Texture[]>();
            dic["1"] = new Texture[] { texture };
            State = "1";
            textureHolder = new TextureHolder(dic);
        }
        public override void Collide(GameObject obj)
        {
            if (obj is Enemy)
            {
                game.Lives--;
            }
        }
    }
}
