using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GameEngine.Render.Texture;
using GameEngine.Logic.Collisions;

namespace GameEngine.Logic
{
    public abstract class GameObject
    {
        private TextureHolder textureHolder;
        public bool Loaded;
        public Body Body;
        public bool Solid;
        public bool Fixed;
        public bool Collidable;
        public string State;
        public int DrawPriority;

        public virtual Texture GetTexture()
        {
            return textureHolder.GetTexture(State);
        }

        public virtual void Collide(GameObject obj)
        {

        }
    }
}
